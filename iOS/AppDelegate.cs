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
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init();

			var manager = new CoreLocation.CLLocationManager();
            manager.RequestWhenInUseAuthorization(); //This is used if you are doing anything in the foreground.

			LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
