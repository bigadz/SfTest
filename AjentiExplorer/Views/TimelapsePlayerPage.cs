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
        private StackLayout ctrlsStack;
		private Grid layoutGrid;
        private bool autoplayPaused = false;
        private int autoplayImageIx = -1;
        private System.Threading.Timer timer;

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
                Minimum = 0,
                Maximum = this.viewModel.ImageUrls.Count - 1,
                RangeStart = 0,
                RangeEnd = 0,
                ShowRange = false,
                Orientation = Orientation.Horizontal,
                TickPlacement = TickPlacement.None,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TickFrequency = 1,
                StepFrequency = 10,
                ShowValueLabel = false,
                SnapsTo = SnapsTo.Ticks,
            };
            this.slider.ValueChanging += async (sender, e) =>
            {
				this.autoplayPaused = true;
				autoPlayBtn.Image = this.viewModel.AutoPlayButtonImageForState(this.autoplayPaused);

				await this.ShowImageIx((int)e.Value);
            };


            this.ctrlsStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    autoPlayBtn,
                    this.slider
                }
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

            this.layoutGrid.Children.Add(this.ctrlsStack, 0, 1, 1, 2);

            Content = this.layoutGrid;
			Device.StartTimer(TimeSpan.FromMilliseconds(500), AutoplayStep);

		}

        private bool AutoplayStep()
        {
            Console.WriteLine($"AutoplayStep - this.autoplayPaused = {this.autoplayPaused}");
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
            if (imageIndex > this.viewModel.ImageUrls.Count-1)
                imageIndex = this.viewModel.ImageUrls.Count - 1;

            // Move image to the top, then fade it in
            this.layoutGrid.Children.Remove(this.images[imageIndex]);
            this.layoutGrid.Children.Insert(this.layoutGrid.Children.Count-1, this.images[imageIndex]);


            await this.images[imageIndex].FadeTo(1);
            this.images[this.currentImageIndex].Opacity = 0;

			this.currentImageIndex = imageIndex;
		}
    }
}

