using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using UI.Extensions;
using UI.Models.User;

namespace UI.Helpers
{
    public class StateHelper : IStateHelper
    {
        const string StateToken = "_Token";
        const string StateUsername = "_Username";
        const string StateFullName = "_FullName";
        const string StateFirstName = "_FirstName";
        const string StateId = "_Id";
        const string StateRole = "_Role";

        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public StateHelper(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            if (appSettings == null) { throw new ArgumentNullException(nameof(appSettings)); }

            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public void ClearState()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);
            tempData.Clear();
            _httpContextAccessor.HttpContext.Session.Clear();
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("StateData");
        }

        public void SetState(Dictionary<string, string> stateDataFromCookie)
        {
            SetTempData(stateDataFromCookie);
            SetSessionData(stateDataFromCookie);
        }

        public void SetState(UserVM authenticatedUser, bool RememberMe)
        {
            if (authenticatedUser == null) { throw new ArgumentNullException(nameof(authenticatedUser)); }

            if (authenticatedUser.Name == null)
            {
                authenticatedUser.Name = authenticatedUser.Username;
            }

            // Validate the JWT token
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var jwt = authenticatedUser.Token;
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var user = handler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                var token = handler.ReadJwtToken(jwt);

                Dictionary<string, string> stateData = new Dictionary<string, string>
                {
                    { StateToken, authenticatedUser.Token },
                    { StateUsername, authenticatedUser.Username },
                    { StateFullName, authenticatedUser.Name + " " + authenticatedUser.LastName },
                    { StateId, authenticatedUser.Id },
                    { StateFirstName, authenticatedUser.Name },
                    { StateRole, authenticatedUser.Role.ToString() }
                };

                SetTempData(stateData);

                SetSessionData(stateData);

                if (RememberMe)
                {
                    SetCookieData(stateData);
                }

                return;
            }
            catch (Exception e)
            {
                throw new Exception("Your authentication attempt has been compromised", e.InnerException);
            }
        }

        private void SetTempData(Dictionary<string, string> stateData)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);
            tempData["StateData"] = stateData;
        }

        private void SetSessionData(Dictionary<string, string> stateData)
        {
            _httpContextAccessor.HttpContext.Session.SetStateData("StateData", stateData);
        }

        private void SetCookieData(Dictionary<string, string> stateData)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            _httpContextAccessor.HttpContext.Response.Cookies.SetStateData("StateData", stateData, cookieOptions);
        }
    }
}