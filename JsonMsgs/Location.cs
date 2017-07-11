using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class Location
    {
		public int id { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public double? latitude { get; set; }
		public double? longitude { get; set; }
		public double? budget { get; set; }
		public List<string> siteTypes { get; set; }
		public List<Meter> meters { get; set; }
		public List<string> features { get; set; }
		public List<Dashboard> dashboards { get; set; }
	}
}
