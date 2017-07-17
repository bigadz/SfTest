using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AjentiExplorer
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            SignInCommand = new Command(async () => await SignIn());
        }

        string message = string.Empty;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }


		public string Username
		{
            get { return Settings.Username; }
            set { Settings.Username = value; OnPropertyChanged(); }
		}

		public string Password
		{
			get { return Settings.Password; }
			set { Settings.Password = value; OnPropertyChanged(); }
		}

		public bool StayLoggedIn
		{
            get { return Settings.StayLoggedIn; }
			set { Settings.StayLoggedIn = value; OnPropertyChanged(); }
		}

        public ICommand SignInCommand { get; }

        async Task SignIn()
        {
            try
            {
                IsBusy = true;
                Message = "Signing In...";

                // Log the user in
                await TryLoginAsync();
            }
            finally
            {
                Message = string.Empty;
                IsBusy = false;

                if (Settings.IsLoggedIn)
                    App.GoToMainPage();
                else
					MessagingCenter.Send(this, "alert", new MessagingCenterAlert
					{
						Title = "Login Failure",
						Message = "Unable to log in",
						Cancel = "OK"
					});
            }
        }

        public static async Task<bool> TryLoginAsync()
        {
            var loginCreds = new JsonMsgs.AccountLoginRequest
			{
                username = Settings.Username,
                password = Settings.Password,
                appname = "AjentiExplorer",
			};
	
            var dataViewApi = new Services.DataViewApi();

            var response = await dataViewApi.LoginAsync(loginCreds);

            Settings.AuthToken = string.Empty;//response.result ? response.token : string.Empty;
            return response.result;
		}
    }
}
