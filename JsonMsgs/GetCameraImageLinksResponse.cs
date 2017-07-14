using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMsgs
{
	public class GetCameraImageLinksResponse : BaseResponse
	{
		public List<CameraImage> images { get; set; }
	}

	public class CameraImage
	{
		public string name { get; set; }

		public List<CameraImageUrl> urls { get; set; }
	}

	public class CameraImageUrl
	{
		public DateTime timeStamp { get; set; }
		public string url { get; set; }
	}
}

