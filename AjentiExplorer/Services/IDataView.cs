using System;
using System.Threading.Tasks;
using JsonMsgs;

namespace AjentiExplorer.Services
{
    public interface IDataView
    {
        Task<AccountLoginResponse> LoginAsync(AccountLoginRequest login);
        Task<AccountLoginResponse> ReauthenticateAsync(ReauthenticateRequest request);
        Task<BaseResponse> ChangePasswordAsync(ChangePasswordRequest request);
        Task<PasswordResetResponse> PasswordResetAsync(PasswordResetRequest request);
        Task<GetCameraImageLinksResponse> GetCameraImageLinksAsync(GetCameraImageLinksRequest request);
        Task<InstallationSearchResponse> InstallationSearchAsync(InstallationSearchRequest request);
        Task<InstallationsInRangeResponse> InstallationsInRangeAsync(InstallationsInRangeRequest request);
        Task<BaseResponse> RecordSiteJournalAsync(RecordSiteJournalRequest request);
        Task<GetSiteJournalResponse> GetSiteJournalAsync(GetSiteJournalRequest request);
        Task<BaseResponse> RecordLocationAsync(RecordLocationRequest request);
    }
}
