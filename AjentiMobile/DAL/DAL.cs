using adms.database.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjentiMobile
{
    public static class DAL
    {
		/// <summary>
		/// The adms API
		/// </summary>
		private static AdmsApi admsApi;

		public static AdmsApi GetAdmsApi()
		{
			if (DAL.admsApi == null)
			{
				string connectionString = "Server=ENTURA-DB02;Database=ADMS;Trusted_Connection=True;";
				//			string connectionString = "Server=localhost;Database=ADMS;Trusted_Connection=True;";
				DAL.admsApi = DAL.CreateAdmsApi(connectionString);
			}
			return DAL.admsApi;
		}

		/// <summary>
		/// Creates the adms API.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns></returns>
		public static AdmsApi CreateAdmsApi(string connectionString, bool securityDisabled = false)
		{
			admsApi = new AdmsApi()
			{
				ConnectionString = connectionString,
				LiveReports = new LiveReports() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				TimeSeriesManagement = new TimeSeriesManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				TimeSeriesDetails = new TimeSeriesDetails() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				ReferenceData = new ReferenceData() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				RemoteSystemControl = new RemoteSystemControl() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				MetaData = new MetaData() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				AccountManagement = new AccountManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				TelemetryManagement = new TelemetryManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				CustomerManagement = new CustomerManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				InstallationManagement = new InstallationManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				IssueManagement = new IssueManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				ScheduleManagement = new ScheduleManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				DashboardManagement = new DashboardManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				WorkflowManagement = new WorkflowManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				LedgerManagement = new LedgerManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				NotificationManagement = new NotificationManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled },
				SecurityDisabled = false,
				
				FileManagement = new FileManagement() { ConnectionString = connectionString, SecurityDisabled = securityDisabled }
			};
			admsApi.AdmsApi = admsApi; // What the?

			(admsApi.LiveReports as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.TimeSeriesManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.TimeSeriesDetails as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.ReferenceData as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.RemoteSystemControl as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.MetaData as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.AccountManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.TelemetryManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.CustomerManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.InstallationManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.IssueManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.ScheduleManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.DashboardManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.WorkflowManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.LedgerManagement as BaseAdmsApi).AdmsApi = admsApi; // I know this one is definitely needed
			(admsApi.NotificationManagement as BaseAdmsApi).AdmsApi = admsApi;
			(admsApi.FileManagement as BaseAdmsApi).AdmsApi = admsApi;

			return admsApi;
		}

	}
}
