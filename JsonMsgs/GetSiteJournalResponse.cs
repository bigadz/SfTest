using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
    public class GetSiteJournalResponse: BaseResponse
    {
		public List<SiteJournalEntry> entries { get; set; }
    }

	public class SiteJournalEntry
	{
		public DateTime time { get; set; }
		public string notes { get; set; }
	}
}
