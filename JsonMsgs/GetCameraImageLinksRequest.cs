using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class GetCameraImageLinksRequest: BaseRequest
    {
		public int id { get; set; }
		public int? imagesOnDisplay { get; set; }
	}
}
