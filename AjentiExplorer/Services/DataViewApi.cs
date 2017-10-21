using System;
using System.Collections.Generic;
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
            this.ajentiMobileApi = new AjentiMobileApi(Settings.ServerEnvironment);
        }

        public async Task<BaseResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            BaseResponse response = null;

			try
			{
                request.token = Settings.AuthToken;
				string body = JsonConvert.SerializeObject(request);
				string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/ChangePassword", body);
				response = JsonConvert.DeserializeObject<BaseResponse>(jsonResponse);
			}
			catch (Exception ex)
			{
				response = new BaseResponse
				{
					result = false,
					message = $"ChangePasswordAsync failed locally - {ex.Message}",
				};
			}

            return response;
		}

        public async Task<GetCameraImageLinksResponse> GetCameraImageLinksAsync(GetCameraImageLinksRequest request)
        {
			GetCameraImageLinksResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/GetCameraImageLinks", body);
				//response = JsonConvert.DeserializeObject<GetCameraImageLinksResponse>(jsonResponse);
                response = new GetCameraImageLinksResponse
                {
                    images = new List<CameraImage>(),
                    result = true,
                };
			}
			catch (Exception ex)
			{
				response = new GetCameraImageLinksResponse
				{
					result = false,
					message = $"GetCameraImageLinksAsync failed locally - {ex.Message}",
				};
			}

			return response;
		}

        public async Task<GetSiteJournalResponse> GetSiteJournalAsync(GetSiteJournalRequest request)
        {
			GetSiteJournalResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/GetSiteJournal", body);
				//response = JsonConvert.DeserializeObject<GetSiteJournalResponse>(jsonResponse);
                response = new GetSiteJournalResponse
                {
                    result = true,
                };
			}
			catch (Exception ex)
			{
				response = new GetSiteJournalResponse
				{
					result = false,
					message = $"GetSiteJournalAsync failed locally - {ex.Message}",
				};
			}

			return response;
		}

        public async Task<InstallationSearchResponse> InstallationSearchAsync(InstallationSearchRequest request)
        {
			InstallationSearchResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/InstallationSearch", body);
				//response = JsonConvert.DeserializeObject<InstallationSearchResponse>(jsonResponse);
                response = new InstallationSearchResponse
                {
                    result = true,
                };
                response.results.Add(new Location
                {
                    name = "Hobart",
                    latitude = -42.8823389,
                    longitude = 147.3110038,
                    id = 1,
                    address  = "Hobart",
                });
			}
			catch (Exception ex)
			{
				response = new InstallationSearchResponse
				{
					result = false,
					message = $"InstallationSearchAsync failed locally - {ex.Message}",
				};
			}

			return response;
		}

        public async Task<InstallationsInRangeResponse> InstallationsInRangeAsync(InstallationsInRangeRequest request)
        {
			InstallationsInRangeResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/InstallationsInRange", body);
				//response = JsonConvert.DeserializeObject<InstallationsInRangeResponse>(jsonResponse);
                response = new InstallationsInRangeResponse
                {
                    result = true,
                };

			}
			catch (Exception ex)
			{
				response = new InstallationsInRangeResponse
				{
					result = false,
					message = $"InstallationsInRangeAsync failed locally - {ex.Message}",
				};
			}

			return response;
		}

        public async Task<AccountLoginResponse> LoginAsync(AccountLoginRequest login)
        {
			AccountLoginResponse response = null;

			try
			{
				//string body = JsonConvert.SerializeObject(login);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/Login", body);
                //response = JsonConvert.DeserializeObject<AccountLoginResponse>(jsonResponse);
                response = new AccountLoginResponse
                {
                    username = login.username,
                    token = "123",
                    result = true,
                };

                // Store the token created by the login
                Settings.AuthToken = response.token;
                Settings.Username = login.username;
                Settings.Password = Settings.StayLoggedIn ? login.password : String.Empty;
			}
			catch (Exception ex)
			{
				response = new AccountLoginResponse
				{
					result = false,
					message = $"LoginAsync failed locally - {ex.Message}",
				};
			}

			return response;
        }

        public async Task<PasswordResetResponse> PasswordResetAsync(PasswordResetRequest request)
        {
			PasswordResetResponse response = null;

			try
			{
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/PasswordReset", body);
				//response = JsonConvert.DeserializeObject<PasswordResetResponse>(jsonResponse);
                response = new PasswordResetResponse
                {
                    token = "123",
                    result = true,
                };
			}
			catch (Exception ex)
			{
				response = new PasswordResetResponse
				{
					result = false,
					message = $"PasswordResetAsync failed locally - {ex.Message}",
				};
			}

			return response;
		}

        public async Task<AccountLoginResponse> ReauthenticateAsync(ReauthenticateRequest request)
        {
			AccountLoginResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/Reauthenticate", body);
				//response = JsonConvert.DeserializeObject<AccountLoginResponse>(jsonResponse);
                response = new AccountLoginResponse
                {
                    username = Settings.Username,
                    token = Settings.AuthToken,
                    result = true,
                };

			}
			catch (Exception ex)
			{
                response = new AccountLoginResponse
                {
					result = false,
				    message = $"ReauthenticateAsync failed locally - {ex.Message}",
			    };
			}

			return response;
		}

        public async Task<BaseResponse> RecordLocationAsync(RecordLocationRequest request)
        {
			BaseResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/RecordLocation", body);
				//response = JsonConvert.DeserializeObject<BaseResponse>(jsonResponse);
                response = new BaseResponse
                {
                    result = true,
                };
			}
			catch (Exception ex)
			{
				response = new BaseResponse
				{
					result = false,
					message = $"RecordLocationAsync failed locally - {ex.Message}",
				};
			}

            return response;
		}

        public async Task<BaseResponse> RecordSiteJournalAsync(RecordSiteJournalRequest request)
        {
			BaseResponse response = null;

			try
			{
				//request.token = Settings.AuthToken;
				//string body = JsonConvert.SerializeObject(request);
				//string jsonResponse = await this.ajentiMobileApi.PostAsync("/DataView/RecordSiteJournal", body);
				//response = JsonConvert.DeserializeObject<BaseResponse>(jsonResponse);
                response = new AccountLoginResponse
                {
                    result = true,
                };
			}
			catch (Exception ex)
			{
				response = new BaseResponse
				{
					result = false,
					message = $"RecordSiteJournalAsync failed locally - {ex.Message}",
				};
			}

            return response;
		}
    }
}
