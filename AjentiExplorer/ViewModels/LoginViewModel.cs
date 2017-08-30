using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AjentiExplorer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand SignInCommand { get; }

        public LoginViewModel()
        {
            Title = "Login";
            SignInCommand = new Command(async () => await SignInAsync());
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

        public async Task SignInAsync()
        {
            try
            {
                IsBusy = true;
                BusyMessage = "Signing In...";

                // Log the user in
                await TryLoginAsync();
            }
            finally
            {
                BusyMessage = string.Empty;
                IsBusy = false;

                if (Settings.IsLoggedIn)
					await App.SwitchToPage(this.Navigation, new Views.SearchMapPage(new SearchMapViewModel())); //App.GoToMainPage();
//				await App.SwitchToPage(this.Navigation, new Views.TimelapsePlayerPage(new TimelapsePlayerViewModel())); //App.GoToMainPage();
                else
					MessagingCenter.Send(this, "alert", new MessagingCenterAlert
					{
						Title = "Login Failure",
						Message = "Unable to log in",
						Cancel = "OK"
					});
            }
        }

		public async Task ReauthenticateAsync()
		{
			try
			{
				IsBusy = true;
				BusyMessage = "Logging In...";

				// Log the user in
				await TryReauthenticateAsync();
			}
			finally
			{
				BusyMessage = string.Empty;
				IsBusy = false;

				if (Settings.IsLoggedIn)
			        await App.SwitchToPage(this.Navigation, new Views.SearchListPage(new SearchListViewModel())); //App.GoToMainPage();
				else
					MessagingCenter.Send(this, "alert", new MessagingCenterAlert
					{
						Title = "Login Failure",
						Message = "Unable to log in",
						Cancel = "OK"
					});
			}
		}

		async Task<bool> TryLoginAsync()
        {
            var loginCreds = new JsonMsgs.AccountLoginRequest
			{
                username = Settings.Username,
                password = Settings.Password,
                appname = "AjentiExplorer",
			};

            var response = await this.dataViewApi.LoginAsync(loginCreds);

            Settings.AuthToken = response.result ? response.token : string.Empty;
            return response.result;
		}

		async Task<bool> TryReauthenticateAsync()
		{
            var request = new JsonMsgs.ReauthenticateRequest();
            var response = await this.dataViewApi.ReauthenticateAsync(request);

			Settings.AuthToken = response.result ? response.token : string.Empty;

			return response.result;
		}
	}
}
