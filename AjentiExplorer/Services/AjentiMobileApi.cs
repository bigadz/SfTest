using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AjentiExplorer.Services
{
    public class AjentiMobileApi
    {
		private HttpClient client;
		private Dictionary<string, string> baseUrls = new Dictionary<string, string>();
		private string baseUrl;

		public AjentiMobileApi(string env)
		{
			this.baseUrls["Prod"] = "https://mobile.ajenti.com.au/";
			this.baseUrl = this.baseUrls[env];
			this.client = new HttpClient();
		}

		public async Task<string> FetchAsync(string url)
		{
			var response = await this.client.GetAsync(new Uri(this.baseUrl + "api" + url));
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(response.ReasonPhrase);
			}
			var contentString = await response.Content.ReadAsStringAsync();
			return contentString;
		}

		public async Task<string> PostAsync(string url, string data)
		{
			var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

			var response = await this.client.PostAsync(new Uri(this.baseUrl + "api" + url), content);
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(response.ReasonPhrase);
			}
			var responseContent = await response.Content.ReadAsStringAsync();
			return responseContent;
		}

	}
}
