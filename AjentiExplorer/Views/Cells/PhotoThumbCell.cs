using System;
using Xamarin.Forms;

namespace AjentiExplorer.Views.Cells
{
    public class PhotoThumbCell: ViewCell
    {
        public PhotoThumbCell()
        {
            var grid = new Grid 
            {
                RowDefinitions = 
                {
                    new RowDefinition { Height = new GridLength(150, GridUnitType.Absolute) }   
                },
                BackgroundColor = Settings.Dark6,
                Margin = new Thickness(5),
			};
            var photoTitle = new Label 
            { 
                FontAttributes = FontAttributes.None, 
                TextColor = Color.White,
                BackgroundColor = Settings.Dark1.MultiplyAlpha(0.5), 
                FontSize = 14,
                VerticalOptions = LayoutOptions.End,
				Margin = new Thickness(5),
			};
			photoTitle.SetBinding(Label.TextProperty, new Binding("Title"));
            var photoImage = new Image 
            {  
                Aspect = Aspect.AspectFill,
            };
			photoImage.SetBinding(Image.SourceProperty, "ImageSource");

			grid.Children.Add(photoImage);
            grid.Children.Add(photoTitle);

            View = grid;
        }
    }
}
