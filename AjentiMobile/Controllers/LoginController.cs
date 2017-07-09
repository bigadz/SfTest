using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JsonMsgs;

namespace AjentiMobile.Controllers
{
	[Produces("application/json")]
	[Route("api/Login")]
	public class LoginController : Controller
	{
		private ILogger logger;

		public LoginController(ILogger<LoginController> logger)
		{
			// Store the direct injection
			this.logger = logger;
		}

		// POST api/values
		[HttpPost]
		public async Task<LoginResponse> LoginAsync([FromBody]LoginRequest credentials)
		{
			LoginResponse response = new LoginResponse();

			await Task.Run(() => { response.IsSuccess = true; });

			return response;
		}
	}
}