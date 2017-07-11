using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjentiMobile
{
	public static class Convert
	{
		#region Dashboards

		public static List<JsonMsgs.Dashboard> ToJson(IEnumerable<adms.admscommon.dataview.webapi.Dashboard> dashboards)
		{
			var result = new List<JsonMsgs.Dashboard>();

			if (dashboards != null)
			{
				foreach (var d in dashboards)
				{
					result.Add(ToJson(d));
				}
			}

			return result;
		}

		public static JsonMsgs.Dashboard ToJson(adms.admscommon.dataview.webapi.Dashboard data)
		{
			if (data == null) return null;

			var dashboard = new JsonMsgs.Dashboard
			{
				id = data.id,
				name = data.name,
				widgets = new List<JsonMsgs.Widget>(),
			};
			if (data.widgets != null)
			{
				foreach (var w in data.widgets)
				{
					dashboard.widgets.Add(ToJson(w));
				}
			}
			return dashboard;
		}

		public static JsonMsgs.Widget ToJson(adms.admscommon.dataview.webapi.Widget data)
		{
			if (data == null) return null;

			var widget = new JsonMsgs.Widget
			{
				id = data.id,
				name = data.name,
				scriptUrl = data.scriptUrl,
				scriptMethod = data.scriptMethod,
				widget = data.widget,
			};

			return widget;
		}

		#endregion // Dashboards

		#region AppNotifications
		public static List<JsonMsgs.AppNotification> ToJson(IEnumerable<adms.admscommon.dataview.webapi.AppNotification> notifications)
		{
			var result = new List<JsonMsgs.AppNotification>();

			if (notifications != null)
			{
				foreach (var n in notifications)
				{
					result.Add(ToJson(n));
				}
			}

			return result;
		}

		public static JsonMsgs.AppNotification ToJson(adms.admscommon.dataview.webapi.AppNotification data)
		{
			if (data == null) return null;

			var notification = new JsonMsgs.AppNotification
			{
				name = data.name,
				mandatory = data.mandatory,
				enabled = data.enabled,
			};

			return notification;
		}
		#endregion // AppNotifications

	}
}
