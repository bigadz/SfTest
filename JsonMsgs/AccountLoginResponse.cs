using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class AccountLoginResponse: BaseResponse
	{
		public int id { get; set; }
		public string username { get; set; }
		public string token { get; set; }
		public string name { get; set; }
		public bool canChangePassword { get; set; }
		public string socketsUrl { get; set; }
		public List<string> roles { get; set; }
		public List<string> licensors { get; set; }
		public List<AppNotification> notifications { get; set; }
		public List<Location> locations { get; set; }
		public List<Dashboard> dashboards { get; set; }
	}
}
