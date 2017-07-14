using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class InstallationSearchResponse: BaseResponse
    {
		public List<Location> results { get; set; }
	}
}
