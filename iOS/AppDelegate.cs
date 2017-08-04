using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace AjentiExplorer.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private CoreLocation.CLLocationManager locationManager;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			new Syncfusion.SfNavigationDrawer.XForms.iOS.SfNavigationDrawerRenderer();
            new Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer();
			new Syncfusion.SfNavigationDrawer.XForms.iOS.SfNavigationDrawerRenderer();

			global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init();

            new Syncfusion.SfBusyIndicator.XForms.iOS.SfBusyIndicatorRenderer();

            this.locationManager = new CoreLocation.CLLocationManager();
			this.locationManager.RequestWhenInUseAuthorization(); //This is used if you are doing anything in the foreground.

            Syncfusion.ListView.XForms.iOS.SfListViewRenderer.Init();
			new Syncfusion.SfCarousel.XForms.iOS.SfCarouselRenderer();

			// Store off the device sizes, so we can access them within Xamarin Forms
			App.DisplayScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;
			App.DisplayScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
			App.DisplayScaleFactor = (double)UIScreen.MainScreen.Scale;

            LoadApplication(new App());

            UIColor youTubeRed = UIColor.FromRGB((float)Settings.YouTubeRed.R, (float)Settings.YouTubeRed.G, (float)Settings.YouTubeRed.B);
            UINavigationBar.Appearance.BarTintColor = youTubeRed;
            //UITabBar.Appearance.SelectedImageTintColor = ColorNavBarTint;
            UITabBar.Appearance.BarTintColor = youTubeRed;

			return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
		}
    }
}
