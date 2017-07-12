using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AjentiExplorer.Services
{
    public class MobileAjentiApi
    {
		private HttpClient client;
		private Dictionary<string, string> baseUrls = new Dictionary<string, string>();
		private string baseUrl;

		public MobileAjentiApi(string env)
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

		//public async Task<string> GetAsync(string url, string body)
		//{
		//	var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
  //          var response = await this.client.Asy new Uri(this.baseUrl + "api" + url), content);
		//	if (!response.IsSuccessStatusCode)
		//	{
		//		throw new ApplicationException(response.ReasonPhrase);
		//	}
		//	var contentString = await response.Content.ReadAsStringAsync();
		//	return contentString;
		//}

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

		/*
  public get(url, body): Observable<any>{
    console.log(this.getBaseUrl() + "api" + url);
    return this.http.get(this.getBaseUrl() + "api" + url, body)
      .map(this.extractData)
      .catch(this.handleError)
  }

  public post(url, body): Observable<any>{
    console.log(this.getBaseUrl() + "api" + url);
    return this.http.post(this.getBaseUrl() + "api" + url, body)
      .map(this.extractData)
      .catch(this.handleError)
  }

  public put(url, body): Observable<any>{
    console.log(this.getBaseUrl() + "api" + url);
    return this.http.put(this.getBaseUrl() + "api" + url, body)
      .map(this.extractData)
      .catch(this.handleError)
  }

  private getBaseUrl(){
    return location.protocol + '//' + location.hostname + (location.port ? ':'+location.port : '') + '/'
  }

  private extractData(res:Response) {
    let body = res.json();
    if (body){
      return body.data || body
    } else {
      return {}
    }
  }

  private handleError(error:any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    let errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
*/
	}
}
