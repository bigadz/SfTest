using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class RecordSiteJournalRequest: BaseRequest
    {
		public int installationId { get; set; }
		public string message { get; set; }
		public bool keepPrivate { get; set; }
	}
}
