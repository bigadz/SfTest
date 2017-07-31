using Plugin.Settings;
using Plugin.Settings.Abstractions;

using Xamarin.Forms;

namespace AjentiExplorer
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

		public static Color YouTubeRed = Color.FromHex("cd181f");

		public static Color Dark6 = Color.FromRgb(28, 30, 31); // VS Studio Mac - Code Window BG
        public static Color Dark5 = Color.FromRgb(36, 36, 36); // VS Studio Mac - Code Window Line Numbers BG
		public static Color Dark4 = Color.FromRgb(48, 48, 48); // VS Studio Mac - Code Window Breakpoint BG
		public static Color Dark3 = Color.FromRgb(71, 71, 71); // VS Studio Mac - Code Window Other Tabs BG
		public static Color Dark2 = Color.FromRgb(82, 82, 82); // VS Studio Mac - Code Window Selected Tab BG
		public static Color Dark1 = Color.FromRgb(72, 75, 85); // VS Studio Mac - Sln Menu Background

        public static Color DarkGray = Color.FromRgb(136, 138, 130); // VS Studio Mac - Code Comments
		public static Color LightGray = Color.FromRgb(191, 191, 191); // VS Studio Mac - Sln Menu Text



		#region Setting Constants
		const string UsernameKey = "username";
		static readonly string UsernameDefault = string.Empty;

		const string PasswordKey = "password";
		static readonly string PasswordDefault = string.Empty;

		const string AuthTokenKey = "authtoken";
        static readonly string AuthTokenDefault = string.Empty;

		const string StayLoggedInKey = "stayloggedin";
		static readonly bool StayLoggedInDefault = true;

		const string LatitudeKey = "latitude";
        static readonly double LatitudeDefault = -42.8823241; // Hobart

		const string LongitudeKey = "longitude";
        static readonly double LongitudeDefault = 147.3197967; // Hobart

		const string InitialPageKey = "initialpage";
		static readonly string InitialPageDefault = string.Empty;

		const string ServerEnvironmentKey = "serverenv";
		static readonly string ServerEnvironmentDefault = "Prod";
		#endregion

		public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(AuthToken);
        public static void Logout() { AuthToken = null; }
		public static string AuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthTokenKey, AuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthTokenKey, value);
            }
        }

        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault(UsernameKey, UsernameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsernameKey, value);
            }
        }

		public static string Password
		{
			get
			{
				return AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(PasswordKey, value);
			}
		}

		public static bool StayLoggedIn
		{
			get
			{
                return AppSettings.GetValueOrDefault(StayLoggedInKey, StayLoggedInDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(StayLoggedInKey, value);
			}
		}

		public static double Latitude
		{
			get
			{
				return AppSettings.GetValueOrDefault(LatitudeKey, LatitudeDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(LatitudeKey, value);
			}
		}

		public static double Longitude
		{
			get
			{
				return AppSettings.GetValueOrDefault(LongitudeKey, LongitudeDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(LongitudeKey, value);
			}
		}

		public static string InitialPage
		{
			get
			{
				return AppSettings.GetValueOrDefault(InitialPageKey, InitialPageDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(InitialPageKey, value);
			}
		}


		public static string ServerEnvironment
		{
			get
			{
				return AppSettings.GetValueOrDefault(ServerEnvironmentKey, ServerEnvironmentDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(ServerEnvironmentKey, value);
			}
		}

	}
}
