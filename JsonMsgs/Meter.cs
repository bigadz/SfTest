using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class Meter
    {
		public string id { get; set; }
		public string channel { get; set; }
		public string source { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string units { get; set; }
		public string uri { get; set; }
		public bool canControl { get; set; }
	}
}
