using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;
using Syncfusion.SfRangeSlider.XForms;


namespace AjentiExplorer.Views
{
    public class TimelapsePlayerPage : ContentPage
    {
        private TimelapsePlayerViewModel viewModel;

        private ObservableRangeCollection<Image> images = new ObservableRangeCollection<Image>();
        private int currentImageIndex = 0;
        private SfRangeSlider slider;
        private Grid ctrlsLayout;
		private Label dateLabel;
		private Label timeLabel;
		private Grid layoutGrid;
        private bool autoplayPaused = false;
        private int autoplayImageIx = -1;

        public TimelapsePlayerPage(TimelapsePlayerViewModel viewModel)
        {
            this.BindingContext = this.viewModel = viewModel;

            SetBinding(TitleProperty, new Binding("Title"));

            var autoPlayBtn = new Button
            {
                Image = this.viewModel.AutoPlayButtonImageForState(this.autoplayPaused),
                Margin = new Thickness(20, 0, 0, 0),
            };
            autoPlayBtn.Clicked += (sender, e) =>
            {
                this.autoplayPaused = !this.autoplayPaused;
                if (!this.autoplayPaused) 
                    this.autoplayImageIx = this.currentImageIndex;

                autoPlayBtn.Image = this.viewModel.AutoPlayButtonImageForState(this.autoplayPaused);
            };

            this.slider = new SfRangeSlider
            {
                TrackColor = Color.White,
                TrackSelectionColor = Settings.YouTubeRed,
                KnobColor = Settings.YouTubeRed,
                Minimum = 0,
                Maximum = this.viewModel.ImageUrls.Count - 1,
                Value = 0,
                ShowRange = false,
                Orientation = Orientation.Horizontal,
                TickPlacement = TickPlacement.None,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TickFrequency = 1,
                SnapsTo = SnapsTo.Ticks,
                ShowValueLabel = false,
                HeightRequest = 70,
            };
            this.slider.ValueChanging += async (sender, e) =>
            {
                if (!this.autoplayPaused)
                {
                    // Pause autoplay when the slider is manually adjusted.
                    this.autoplayPaused = true;
                    autoPlayBtn.Image = this.viewModel.AutoPlayButtonImageForState(this.autoplayPaused);
                }

				await this.ShowImageIx((int)e.Value);
            };

			this.ctrlsLayout = new Grid
			{
				RowDefinitions =
				{
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				},
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				},

				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children =
				{
					autoPlayBtn,
					this.slider
				}
			};
            this.ctrlsLayout.Children.Add(autoPlayBtn, 0, 1, 0, 1);
			this.ctrlsLayout.Children.Add(this.slider, 1, 2, 0, 1);

			this.dateLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.End };
			this.timeLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.End };
            this.dateLabel.SetBinding(Label.TextProperty, new Binding("DateString"));
			this.timeLabel.SetBinding(Label.TextProperty, new Binding("TimeString"));
            var infoFrame = new Frame
            {
                CornerRadius = 3,
                BackgroundColor = Settings.DarkGray.MultiplyAlpha(0.3),
                Padding = new Thickness(4),
				Margin = new Thickness(5),
                HasShadow = true,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Start,
				Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = Color.Transparent,
                    Margin = new Thickness(0),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        this.timeLabel,
                        this.dateLabel,
                    },
                },
            };

			this.layoutGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },
            };


            for (int imageIndex = 0; imageIndex < this.viewModel.ImageUrls.Count; imageIndex++)
            {
                var image = new Image
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Aspect = Aspect.AspectFill,
                    Opacity = (imageIndex == 0) ? 1 : 0,
                    Source = new UriImageSource { Uri = new Uri(this.viewModel.ImageUrls[imageIndex]) },
                };
                this.images.Add(image);
                this.layoutGrid.Children.Add(image, 0, 1, 0, 2);
            }

			this.layoutGrid.Children.Add(this.ctrlsLayout, 0, 1, 1, 2);
			this.layoutGrid.Children.Add(infoFrame);

			Content = this.layoutGrid;
			Device.StartTimer(TimeSpan.FromMilliseconds(500), AutoplayStep);

		}

        private bool AutoplayStep()
        {
			if (this.autoplayPaused) return true;

			this.autoplayImageIx++;
            if (this.autoplayImageIx > this.viewModel.ImageUrls.Count - 1)
			{
				this.autoplayImageIx = 0;
			}
            this.slider.Value = this.autoplayImageIx;
			this.ShowImageIx(this.autoplayImageIx); // Don't bother awaiting
            return true;
        }

        private async Task ShowImageIx(int imageIndex)
        {
            // Verify there is a change.
            if (imageIndex == this.currentImageIndex)
                return;
            
            if (imageIndex > this.viewModel.ImageUrls.Count-1)
                imageIndex = this.viewModel.ImageUrls.Count - 1;
            this.viewModel.CurrentImageIndex = imageIndex;

			// Move image to the top (below infoFrame and ctrlsLayout), then fade it in
			this.layoutGrid.Children.Remove(this.images[imageIndex]);
            this.layoutGrid.Children.Insert(this.layoutGrid.Children.Count-2, this.images[imageIndex]);


            await this.images[imageIndex].FadeTo(1);
            this.images[this.currentImageIndex].Opacity = 0;

			this.currentImageIndex = imageIndex;
		}
    }
}

