using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class AppNotification
    {
		public string name { get; set; }
		public bool enabled { get; set; }
		public bool mandatory { get; set; }
	}
}
