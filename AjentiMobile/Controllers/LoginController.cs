using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JsonMsgs;
using adms.admscommon.dto;
using adms.database.api;
using common.security;
using common.utilities;

namespace AjentiMobile.Controllers
{
	[Produces("application/json")]
	[Route("api/Login")]
	public class LoginController : Controller
	{
#region Plumbing
		private ILogger logger;
		private AdmsApi AdmsApi;

		public LoginController(ILogger<LoginController> logger)
		{
			// Store the direct injection
			this.logger = logger;
			this.AdmsApi = DAL.GetAdmsApi();
		}
		#endregion Plumbing

		public AccountLoginResponse GetUserDetails(DTOSsoToken token)
		{
			var account = token.Account;
			bool accountIsNull = (account == null);
			logger.LogInformation($"GetUserDetails (token) token.Token={token.Token}, token.AppId={token.AppId}, accountIsNull={accountIsNull}");
			return GetUserDetails(token.Token, token.AppId, account);
		}

		[NonAction]
		public AccountLoginResponse GetUserDetails(string token, int appId, DTOAccount account)
		{
			logger.LogInformation($"GetUserDetails (1,2,3) account.AccountId={account.AccountId}");
			DTOUserDetails user = null;
			try
			{
				bool hasAccountManagement = AdmsApi.AccountManagement != null;
				logger.LogInformation($"AdmsApi.AccountManagement = {hasAccountManagement}");
				user = AdmsApi.AccountManagement.GetUserDetails(account.AccountId);
			}
			catch (Exception ex)
			{
				logger.LogError($"AdmsApi.AccountManagement.GetUserDetails by AccountId failed = {ex.Message}");

				try
				{
					user = AdmsApi.AccountManagement.GetUserDetails(account.Username);
				}
				catch (Exception ex2)
				{
					logger.LogError($"AdmsApi.AccountManagement.GetUserDetails by Username also failed = {ex2.Message}");
					throw ex;
				}
			}

			logger.LogInformation("GetUserDetails user");
			var securityUser = AdmsApi.AccountManagement.GetSecurityUser(account.Username);
			logger.LogInformation("GetUserDetails securityUser");
			var subscriptions = AdmsApi.InstallationManagement.GetInstallationsBySubscription(account.AccountId);
			logger.LogInformation("GetUserDetails subscriptions");
			var security = AdmsApi.AccountManagement.GetAccountRoles(securityUser.AccountId);
			logger.LogInformation("GetUserDetails security");
			var metaTags = AdmsApi.MetaData.GetMetaTagTree();
			logger.LogInformation("GetUserDetails metaTags");
			var usersNotifications = AdmsApi.NotificationManagement.GetAllSubscriptionsForAccount(account.AccountId, appId);
			logger.LogInformation("GetUserDetails usersNotifications");
			if (usersNotifications == null) logger.LogInformation("GetUserDetails usersNotifications == null");
			var roles = new List<string>();
			foreach (var role in security.Select(r => r.RoleName).Distinct())
			{
				roles.Add(role);
			}
			logger.LogInformation("GetUserDetails roles");

			List<Location> locations = new List<Location>();
			if (subscriptions.Count == 0)
			{
				// no Resident role - go away!
				logger.LogWarning($"The account {account.Username} has logged in but does not have any subscriptions!!!!");
				//return null;
			}
			else
			{
				roles.Add("Resident");
				var siteDashboards = this.AdmsApi.DashboardManagement.GetDashboardsForCurrentUserAndSiteTypes(
						securityUser, subscriptions.SelectMany(l => l.SiteTypeMetaTagIds).ToList(), false);
				locations = SetupLocations(securityUser, subscriptions, security, metaTags, siteDashboards);
				logger.LogInformation("GetUserDetails locations");
			}

			//if (account.Username.ToLower() == "frankspaul" || account.Username.ToLower() == "clarknigel" || account.Username.ToLower() == "clis_test")
			//{
			//	roles.Add("Administrator");
			//}

			var notifications = usersNotifications.Select(n => new AppNotification()
			{
				enabled = n.Enabled,
				name = n.Name,
				mandatory = n.Mandatory
			});
			logger.LogInformation("GetUserDetails notifications");

			#region Setup Dashboards
			// does the user have any dashboards??
			List<adms.admscommon.dataview.webapi.Dashboard> dashboards = null;//new List<adms.admscommon.dataview.webapi.Dashboard>();
			try
			{
				var d = this.AdmsApi.DashboardManagement.GetDashboardsForUser(account.AccountId).Where(dash => dash.SiteTypeId == null).ToList();
				dashboards = GetDashboardConfig(d);
				logger.LogInformation("GetUserDetails dashboards");

			}
			catch (Exception ex)
			{
				logger.LogError($"Failed to setup dashboards for {account.Username} - {ex.Message}");
			}
			#endregion

			return new AccountLoginResponse()
			{
				result = true,
				name = user.Name,
				username = account.Username,
				id = account.AccountId,
				canChangePassword = user.CanChangePassword,
				roles = roles,
				locations = locations,
				dashboards = Convert.ToJson(dashboards),
				notifications = notifications.ToList(),
				token = token
			};
		}

		private static List<adms.admscommon.dataview.webapi.Dashboard> GetDashboardConfig(List<DTODashboard> d)
		{
			var dashboards = new List<adms.admscommon.dataview.webapi.Dashboard>();
			foreach (var dashboard in d.OrderBy(a => a.Name))
			{
				var dd = new adms.admscommon.dataview.webapi.Dashboard()
				{
					id = dashboard.DashboardId,
					name = dashboard.Name
				};
				var widgets = new List<adms.admscommon.dataview.webapi.Widget>();
				foreach (var widget in dashboard.GetAllWidgets().Where(w => !string.IsNullOrEmpty(w.ScriptUrl)))
				{
					var ww = new adms.admscommon.dataview.webapi.Widget()
					{
						id = widget.DashboardItemId,
						name = widget.Name,
						widget = widget.WidgetName,
						scriptUrl = widget.ScriptUrl,
						scriptMethod = widget.ScriptMethod
					};
					widgets.Add(ww);
				}
				dd.widgets = widgets.ToArray();
				if (dd.widgets.Length > 0)
				{
					dashboards.Add(dd);
				}
			}
			return dashboards;
		}

		private List<Location> SetupLocations(SecurityUser account, List<DTOInstallation> allowed, List<DTOAccountRole> roles, DTORootMetaTag metaTags, List<DTODashboard> dashboardConfig)
		{
			var locations = new List<Location>();

			//var allowedTs = roles.Where(s => s is DTOSubscriberAccountRole).SelectMany(s => ((DTOSubscriberAccountRole)s).TimeSeriesIds);

			var siteTypes = AdmsApi.MetaData.GetMetaTagTree().FindMetaTagByPath("Site Type");
			var smartMeter = siteTypes.ChildrenFlat.First(mt => mt.TagName == "Smart Meter");
			var waterMeter = siteTypes.ChildrenFlat.First(mt => mt.TagName == "Water Meter");
			var weatherStation = siteTypes.ChildrenFlat.First(mt => mt.TagName == "Weather Station");
			var gateControl = siteTypes.ChildrenFlat.First(mt => mt.TagName == "Gate Controller");
			var camera = siteTypes.ChildrenFlat.First(mt => mt.TagName == "Camera");
			var hub = siteTypes.ChildrenFlat.First(mt => mt.TagName == "Hub");

			#region Setup Locations
			foreach (var ssg in allowed.GroupBy(g => g.InstallationId).OrderBy(g => g.First().InstallationName))
			{
				try
				{
					var l = ssg.First();

					var location = new Location()
					{
						id = l.InstallationId,
						name = l.FriendlyName ?? ssg.First().InstallationName,
						address = l.Address,
						latitude = l.Coordinate != null && l.Coordinate.IsValidCoordinate() ? l.Coordinate.Latitude : (double?)null,
						longitude = l.Coordinate != null && l.Coordinate.IsValidCoordinate() ? l.Coordinate.Longitude : (double?)null
					};
					//locations.Add(location);
					var features = new List<string>();
					var types = new List<string>();

					#region Setup Dashboards
					// does the user have any dashboards??
					try
					{
						if (account.CanAccessDashboards && l.SiteTypeMetaTagIds != null && l.SiteTypeMetaTagIds.Count > 0)
						{
							var dashboards = GetDashboardConfig(dashboardConfig.Where(d => l.SiteTypeMetaTagIds.Contains(d.SiteTypeId.Value)).ToList());
							location.dashboards = Convert.ToJson(dashboards);
						}
					}
					catch (Exception ex)
					{
						logger.LogError($"Failed to setup dashboards for site {l.InstallationId} - {ex.Message}");
					}
					#endregion

					// add any features that have been specifically enabled for this site

					#region Meta Tag Debug

					if (logger.IsEnabled(LogLevel.Debug))
					{
						logger.LogDebug($"Installation MetaTags {l.InstallationMetaTags.Count} for {l.InstallationName}");
						foreach (var mt in l.InstallationMetaTags)
						{
							try
							{
								if (mt.MetaTag != null)
									logger.LogDebug(mt.MetaTag.TagPath + ' ' + mt.PinnedMetaTagIds == null
										? "0"
										: mt.PinnedMetaTags.Length.ToString());
							}
							catch (Exception debugEx)
							{
								logger.LogWarning($"Error generating debug output {debugEx.Message}");
							}
						}
					}

					#endregion

					#region Setup Features

					var mtFeatures = l.InstallationMetaTags.FirstOrDefault(i => i.MetaTag.TagPath == "Data View/Features");
					if (mtFeatures != null && mtFeatures.PinnedMetaTagIds != null && mtFeatures.PinnedMetaTagIds.Length > 0)
					{
						foreach (var pinnedId in mtFeatures.PinnedMetaTagIds)
						{
							var pinned = metaTags.FindMetaTagById(pinnedId);
							logger.LogDebug($"Adding feature {pinned.TagName}");
							features.Add(pinned.TagName.ToLower());
						}
						logger.LogDebug($"Feature MetaTagId Count = {mtFeatures.PinnedMetaTagIds.Length}");
					}
					else
					{
						logger.LogInformation($"No features specified for installation {l.InstallationName}");

					}

					#endregion

					#region Camera

					if (l.SiteTypeMetaTagIds.Contains(camera.MetaTagId))
					{
						types.Add("Camera");
						locations.Add(location);
					}

					#endregion

					#region Smart Meter

					if (l.SiteTypeMetaTagIds.Contains(smartMeter.MetaTagId))
					{
						types.Add("Smart Meter");
						features.Add("energy-analysis");
						features.Add("energy-insights");
						locations.Add(location);
						// if there is a budget 
						var mtBudget = l.InstallationMetaTags.FirstOrDefault(i => i.MetaTag.TagPath == "Data View/Energy/Budget");
						if (mtBudget != null)
						{
							Safe.Run(() => location.budget = string.IsNullOrEmpty(mtBudget.Tag) ? 0 : double.Parse(mtBudget.Tag));
						}
						var meters = new List<Meter>();

						////////// We don't have access to types DTOEsBoxInstallationConfiguration and DTOEsBoxEndPoint
						//////////
						//foreach (var ss in ssg)
						//{
						//	// if the config is an EsBox, we can check if control is possible
						//	var config = ss.InstallationConfiguration is DTOEsBoxInstallationConfiguration
						//		? (DTOEsBoxInstallationConfiguration)ss.InstallationConfiguration
						//		: null;
						//	foreach (
						//		var ts in
						//			ss.TimeSeries.Where(ts => ts.TimeSeriesUnitAbbr == "W" && ts.TimeSeriesUnit == "Watts").OrderBy(ts => ts.TimeSeriesName))
						//	{
						//		var detail = config == null
						//			? (DTOEsBoxEndPoint)null
						//			: config.EndPoints.FirstOrDefault(e => e.LiveUri == ts.LiveUri);

						//		//if (allowedTs.Contains(ts.TimeSeriesId))
						//		{
						//			meters.Add(new Meter()
						//			{
						//				id = ts.SourceReference,
						//				name = ts.TimeSeriesName,
						//				units = "W",
						//				uri = ts.LiveUri,
						//				canControl = detail == null ? false : detail.CanControl
						//			});
						//		}
						//	}
						//}
						location.meters = meters.OrderBy(m => m.name).ToList();
					}

					#endregion

					#region Water Meter

					if (l.SiteTypeMetaTagIds.Contains(waterMeter.MetaTagId))
					{
						types.Add("Water Meter");
						features.Add("water-analysis");
						features.Add("water-insights");
						locations.Add(location);
						var meters = new List<Meter>();
						foreach (var ss in ssg)
						{
							foreach (var ts in ss.TimeSeries.Where(ts => ts.TimeSeriesUnitAbbr == "Ml" && ts.TimeSeriesUnit == "Megalitres"))
							{
								meters.Add(new Meter()
								{
									id = ts.TimeSeriesId.ToString(),
									source = ts.SourceReference,
									channel = ts.Channel,
									name = ts.TimeSeriesName,
									description = ts.FriendlyName,
									units = "Ml"
								});
							}
						}
						location.meters = meters.OrderBy(m => m.name).ToList();
					}

					#endregion

					#region Weather Station

					if (l.SiteTypeMetaTagIds.Contains(weatherStation.MetaTagId))
					{
						types.Add("Weather Station");
						features.Add("weather-radar");
						features.Add("weather-analysis");
						locations.Add(location);
						var meters = new List<Meter>();

						location.meters = meters.OrderBy(m => m.name).ToList();
					}

					#endregion

					#region Gate Controller

					if (l.SiteTypeMetaTagIds.Contains(gateControl.MetaTagId))
					{
						types.Add("Gate Controller");
						//location.siteType = "WeatherStation";
						features.Add("live");
						features.Add("water-gatecontrol");
						locations.Add(location);
						var meters = new List<Meter>();

						// meters include: Requested Flow, Current Flow, Gate State, Alarm
						var requestedFlow = l.TimeSeries.FirstOrDefault(ts => ts.TimeSeriesName == "Requested Flow");
						var currentFlow = l.TimeSeries.FirstOrDefault(ts => ts.TimeSeriesName == "Current Flow");
						var gateState = l.TimeSeries.FirstOrDefault(ts => ts.TimeSeriesName == "Gate State");
						if (requestedFlow != null)
						{
							meters.Add(new Meter()
							{
								id = requestedFlow.TimeSeriesId.ToString(),
								name = requestedFlow.TimeSeriesName,
								units = "ML/d",
								uri = requestedFlow.LiveUri,
								canControl = account.IsInLicensorRole(Role.FieldCoordinator, l.LicensorCode)
							});
						}
						if (currentFlow != null)
						{
							meters.Add(new Meter()
							{
								id = currentFlow.TimeSeriesId.ToString(),
								name = currentFlow.TimeSeriesName,
								units = "ML/d",
								uri = currentFlow.LiveUri,
								canControl = false
							});
						}
						if (gateState != null)
						{
							meters.Add(new Meter()
							{
								id = gateState.TimeSeriesId.ToString(),
								name = gateState.TimeSeriesName,
								units = "State",
								uri = gateState.LiveUri,
								canControl = false
							});
						}

						location.meters = meters.OrderBy(m => m.name).ToList();
					}

					#endregion

					#region Hub

					if (l.SiteTypeMetaTagIds.Contains(hub.MetaTagId))
					{
						types.Add("Hub");
						features.Add("field-hubmanager");
						features.Add("field-hubhealth");
						locations.Add(location);
						var meters = new List<Meter>();

						var currentlyReporting = l.TimeSeries.FirstOrDefault(ts => ts.TimeSeriesName == "Currently Reporting");
						if (currentlyReporting != null)
						{
							meters.Add(new Meter()
							{
								id = currentlyReporting.TimeSeriesId.ToString(),
								name = currentlyReporting.TimeSeriesName,
								units = "Units",
								uri = currentlyReporting.LiveUri,
							});
						}
					}

					#endregion

					#region Generic

					if (types.Count == 0)
					{
						types.Add("Generic");
						locations.Add(location);
					}

					#endregion

					location.siteTypes = types;
					location.features = features;
				}
				catch (Exception ex)
				{
					logger.LogError($"Failed to setup location {ssg.Key} - {ex.Message}");
				}
			}
			#endregion

			return locations;
		}

		internal bool ValidateLogOn(string userName, string password)
		{
			if (String.IsNullOrEmpty(userName))
			{
				ModelState.AddModelError("username", "You must specify a username.");
			}
			if (String.IsNullOrEmpty(password))
			{
				ModelState.AddModelError("password", "You must specify a password.");
			}
			if (AdmsApi.AccountManagement.AuthenticateUser(userName, password) == null)
			{
				ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
			}

			return ModelState.IsValid;
		}

		// POST api/values
		[HttpPost]
		public async Task<AccountLoginResponse> LoginAsync([FromBody]AccountLoginRequest login)
		{
			logger.LogInformation($"LoginController.LoginAsync({login.username})");

			AccountLoginResponse response = new AccountLoginResponse();

			logger.LogInformation("New response created.");

			try
			{
				logger.LogInformation("AdmsApi.Start()");
				this.AdmsApi.Start();
				logger.LogInformation("started.");
			}
			catch (Exception ex)
			{
				logger.LogError($"Failed to start AdmsApi. Reason: {ex.Message}");
				return response;
			}

			await Task.Run(() => 
			{
				try
				{
					logger.LogInformation("Bg thread.");
					var token = AdmsApi.AccountManagement.GenerateAppAuthenticationToken(
						login.username, login.password, string.IsNullOrEmpty(login.appname) ? "AjentiExplorer" : login.appname,
						(int)TimeSpan.FromDays(10).TotalSeconds);
					logger.LogInformation("Token created.");

					if (token == null)
					{
						logger.LogWarning("LoginController.LoginAsync token == null");
						this.HttpContext.Response.StatusCode = 401;
						response.result = false;
						response.message = "Unauthorised";
					}
					else
					{
						logger.LogInformation($"LoginController.LoginAsync token == {token.Account.Name}");
						response = this.GetUserDetails(token);
					}
				}
				catch (Exception ex)
				{
					logger.LogError($"Failed to Login {login.username} - {ex.Message}");
					response.result = false;
					response.message = "Error During Login Attempt";
				}
			});

			return response;
		}
	}
}