using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class LocationSearchResponse: BaseResponse
    {
		public List<Location> results { get; set; }
	}
}
