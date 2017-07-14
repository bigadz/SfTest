using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class RecordLocationRequest: BaseRequest
    {
		public float latitude { get; set; }
		public float longitude { get; set; }
	}
}
