using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class ChangePasswordRequest: BaseRequest
    {
		public string oldPassword { get; set; }

		public string newPassword { get; set; }
	}
}
