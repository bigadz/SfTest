using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class GetSiteJournalRequest: BaseRequest
    {
		public int installationId { get; set; }
		public DateTime from { get; set; }
		public DateTime to { get; set; }
	}
}
