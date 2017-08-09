using System;
using AjentiExplorer.Models;
using Xamarin.Forms;

namespace AjentiExplorer.Views.Cells
{
    public class DrawerMenuCell: ViewCell
    {
        //public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(DrawerMenuCell), "Title");
        Label menuText;

		//public string Title
		//{
		//	get { return (string)GetValue(TitleProperty); }
		//	set { SetValue(TitleProperty, value); }
		//}

        public DrawerMenuCell()
        {

			//instantiate each of our views
			//var image = new Image();
			//StackLayout cellWrapper = new StackLayout
			//{
			//    HorizontalOptions = LayoutOptions.FillAndExpand,
			//    VerticalOptions = LayoutOptions.FillAndExpand,
			//    Orientation = StackOrientation.Vertical,
			//};
			//StackLayout horizontalLayout = new StackLayout();
			this.menuText = new Label
            { 
                TextColor = Settings.LightGray,
                Margin = new Thickness(40, 15, 20, 5),
			};

			//set bindings
			this.menuText.SetBinding(Label.TextProperty, "Title");
			//image.SetBinding(Image.SourceProperty, "Image");

			//Set properties for desired design
			//cellWrapper.BackgroundColor = Settings.Dark1;
			//horizontalLayout.Orientation = StackOrientation.Horizontal;
			//horizontalLayout.BackgroundColor = Settings.Dark3;

			//add views to the view hierarchy
			//horizontalLayout.Children.Add(image);
			//horizontalLayout.Children.Add(menuText);
			//cellWrapper.Children.Add(menuText);
            View = this.menuText;
        }

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
                var drawMenuItem = BindingContext as DrawerMenuItem;
                this.menuText.TextColor = drawMenuItem.Current ? Color.White : Settings.LightGray;
			}
		}
    }
}
