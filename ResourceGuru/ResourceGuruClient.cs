using ResourceGuru.Authentication;
using ResourceGuru.Services;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Web;
using ResourceGuru.Utils.Authentication;
using System.Net;

namespace ResourceGuru
{
    public class ResourceGuruClient
    {
        protected string ClientId { get; set; }
        protected string ClientSecret { get; set; }
        protected string ApiUrl { get; set; }
        public OAuthInfo OAuthInfo { get; set; }

        public WebProxy Proxy { get; set; }

        public IAuthStore AuthStore { get; set; }
        private RequestHelper requestHelper 
        { 
            get { return new RequestHelper(this.OAuthInfo,this,this.Proxy); } 
        }

        public ResourceGuruClient(string clientId, string clientSecret, IAuthStore authStore = null, WebProxy proxy = null)
        {
            Proxy = proxy;
            ClientId = clientId;
            ClientSecret = clientSecret;
            if (authStore != null)
                AuthStore = authStore;
            else
                AuthStore = new NullAuthStore();

            OAuthInfo = AuthStore.Get();
        }

        public string GetAuthorizeUrl(string redirectUri)
        {
            string authorizeUrl = "https://api.resourceguruapp.com/oauth/authorize?response_type=code&client_id={0}&redirect_uri={1}";
            return String.Format(authorizeUrl, this.ClientId, HttpUtility.UrlEncode(redirectUri));
        }

        public OAuthInfo AuthenticateWithAuthorizationCode(string authorizationCode, string redirectUri)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"code", authorizationCode},
                   {"redirect_uri", redirectUri},
                   {"grant_type", "authorization_code"}
                };
            return Authenticate("authorization_code", authRequest);
        }

        public OAuthInfo AuthenticateWithPassword(string username, string password)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"username", username},
                   {"password", password},
                   {"grant_type", "password"}
                };
            return Authenticate("password", authRequest);
        }

        public OAuthInfo RefreshAccessToken(OAuthInfo authinfo)
        {
            this.OAuthInfo = authinfo;
            var authRequest = new Dictionary<string, string>(){
                   {"refresh_token", OAuthInfo.refresh_token},
                   {"grant_type", "refresh_token"}
                };
            return Authenticate("refresh_token", authRequest);
        }

        /// <summary>
        /// Check if there is a stored access token already present.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return (this.OAuthInfo != null && !string.IsNullOrEmpty(this.OAuthInfo.access_token));
        }

        private OAuthInfo Authenticate(string grantType, Dictionary<string, string> attributes)
        {
            attributes["client_id"] = ClientId;
            attributes["client_secret"] = ClientSecret;
            Dictionary<string, object> options = null;

            options = new Dictionary<string, object>(){
                {"oauth_request",true}
                };

            OAuthInfo OAuthInfo = requestHelper.Post<OAuthInfo>("/oauth/token", attributes, options);
            this.OAuthInfo = OAuthInfo;
            AuthStore.Set(OAuthInfo);

            return OAuthInfo;
        }

        #region Services
        public AccountService AccountService
        {
            get { return new AccountService(this.requestHelper); }
        }

        public BookingService BookingService
        {
            get { return new BookingService(this.requestHelper); }
        }

        public ClientService ClientService
        {
            get { return new ClientService(this.requestHelper); }
        }

        public ProjectService ProjectService
        {
            get { return new ProjectService(this.requestHelper); }
        }

        public ResourceService ResourceService
        {
            get { return new ResourceService(this.requestHelper); }
        }

        public TimeOffService TimeOffService
        {
            get { return new TimeOffService(this.requestHelper); }
        }

        public UserService UserService
        {
            get { return new UserService(this.requestHelper); }
        }
        public ResourceTypeService ResourceTypeService
        {
            get { return new ResourceTypeService(this.requestHelper); }
        }

        public HookService HookService
        {
            get { return new HookService(this.requestHelper); }
        }
        #endregion
    }
}
