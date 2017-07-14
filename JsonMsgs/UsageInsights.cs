using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
	public class UsageInsights : BaseResponse
	{
		public string name { get; set; }
		public string installationId { get; set; }
		public List<UsageInsight> insights { get; set; }
	}

	public class UsageInsight
	{
		public string name { get; set; }
		public string type { get; set; }
		public string description { get; set; }
	}

	public class UsageInsightRange : UsageInsight
	{
		public string start { get; set; }
		public string end { get; set; }
	}

	public class UsageInsightEvent : UsageInsight
	{
		public string value { get; set; }
	}

}
