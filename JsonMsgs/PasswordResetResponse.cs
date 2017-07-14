using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class PasswordResetResponse: BaseResponse
	{
		public string token { get; set; }
	}
}
