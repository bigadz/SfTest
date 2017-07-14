using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class InstallationsInRangeRequest: BaseRequest
    {
		public double latitude { get; set; }
		public double longitude { get; set; }
		public double distance { get; set; }
	}
}
