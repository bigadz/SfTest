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
		public static AdmsApi CreateAdmsApi(string connectionString)
		{
			admsApi = new AdmsApi()
			{
				ConnectionString = connectionString,
				LiveReports = new LiveReports() { ConnectionString = connectionString },
				TimeSeriesManagement = new TimeSeriesManagement() { ConnectionString = connectionString },
				TimeSeriesDetails = new TimeSeriesDetails() { ConnectionString = connectionString },
				ReferenceData = new ReferenceData() { ConnectionString = connectionString },
				RemoteSystemControl = new RemoteSystemControl() { ConnectionString = connectionString },
				MetaData = new MetaData() { ConnectionString = connectionString },
				AccountManagement = new AccountManagement() { ConnectionString = connectionString },
				TelemetryManagement = new TelemetryManagement() { ConnectionString = connectionString },
				CustomerManagement = new CustomerManagement() { ConnectionString = connectionString },
				InstallationManagement = new InstallationManagement() { ConnectionString = connectionString },
				IssueManagement = new IssueManagement() { ConnectionString = connectionString },
				ScheduleManagement = new ScheduleManagement() { ConnectionString = connectionString },
				DashboardManagement = new DashboardManagement() { ConnectionString = connectionString },
				WorkflowManagement = new WorkflowManagement() { ConnectionString = connectionString },
				LedgerManagement = new LedgerManagement() { ConnectionString = connectionString },
				//FileManagement = new FileManagement() { ConnectionString = connectionString }
			};
			admsApi.AdmsApi = admsApi; // What the?
			(admsApi.LedgerManagement as BaseAdmsApi).AdmsApi = admsApi;

			return admsApi;
		}

	}
}
