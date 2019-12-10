using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UI.Extensions;
using UI.Helpers;

namespace UI.Handlers
{
    // Intercepts every HttpClient request to the web API and add the bearer JWT token to every request authorization header
    // Exceptions are anonymous web API URL's where no token is required such as for registration and login 
    public class ValidateHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStateHelper _stateHelper;
        private string token = null;

        public ValidateHeaderHandler
        (
            IHttpContextAccessor httpContextAccessor,
            IStateHelper stateHelper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _stateHelper = stateHelper;
        }

        protected override async Task<HttpResponseMessage> SendAsync
        (
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            // If StateData such as username and JWT token is available in the session, use those values
            if (_httpContextAccessor.HttpContext.Session.GetStateData("StateData") != null)
            {
                token = _httpContextAccessor.HttpContext.Session.GetStateData("StateData")["_Token"];

                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            else // If StateData such as username and JWT token is available in the cookie, use those values
            {
                if (_httpContextAccessor.HttpContext.Request.GetStateData("StateData") != null)
                {
                    // Set StateData in session, cookie and TempData
                    _stateHelper.SetState(_httpContextAccessor.HttpContext.Request.GetStateData("StateData"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
