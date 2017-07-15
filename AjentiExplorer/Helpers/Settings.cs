using Plugin.Settings;
using Plugin.Settings.Abstractions;

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
