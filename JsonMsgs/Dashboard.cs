using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class Dashboard
    {
		public int id { get; set; }
		public string name { get; set; }
		public List<Widget> widgets { get; set; }
	}
}
