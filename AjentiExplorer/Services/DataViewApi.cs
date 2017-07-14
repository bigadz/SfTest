using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonMsgs;

namespace AjentiExplorer.Services
{
    public class DataViewApi: IDataView
    {
        private AjentiMobileApi ajentiMobileApi;

        public DataViewApi()
        {
            this.ajentiMobileApi = new AjentiMobileApi("Prod");
        }

        internal string Token { get; set; }

        public async Task<BaseResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            BaseResponse response = null;

			try
			{
				request.token = this.Token;
				string body = JsonConvert.SerializeObject(request);
				string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/Login", body);
				response = JsonConvert.DeserializeObject<BaseResponse>(jsonResponse);
			}
			catch (Exception ex)
			{
                response.result = false;
                response.message = $"ChangePasswordAsync failed locally - {ex.Message}";
			}

            return response;
		}

        public async Task<GetCameraImageLinksResponse> GetCameraImageLinksAsync(GetCameraImageLinksRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetSiteJournalResponse> GetSiteJournalAsync(GetSiteJournalRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallationSearchResponse> InstallationSearchAsync(InstallationSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallationsInRangeResponse> InstallationsInRangeAsync(InstallationsInRangeRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountLoginResponse> LoginAsync(AccountLoginRequest login)
        {
			AccountLoginResponse response = null;

			try
			{
				string body = JsonConvert.SerializeObject(login);
				string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/Login", body);
				response = JsonConvert.DeserializeObject<AccountLoginResponse>(jsonResponse);
			}
			catch (Exception ex)
			{
				response.result = false;
				response.message = $"Login failed locally - {ex.Message}";
			}

			return response;
        }

        public async Task<PasswordResetResponse> PasswordResetAsync(PasswordResetRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountLoginResponse> ReauthenticateAsync(ReauthenticateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> RecordLocationAsync(RecordLocationRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> RecordSiteJournalAsync(RecordSiteJournalRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
