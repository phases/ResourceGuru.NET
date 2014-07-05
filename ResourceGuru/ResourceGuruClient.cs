using ResourceGuru.Authentication;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ResourceGuru
{
    public class ResourceGuruClient
    {
        protected string ClientId { get; set; }
        protected string ClientSecret { get; set; }
        protected string ApiUrl { get; set; }
        protected OAuthInfo OAuthInfo { get; set; }

        public RequestHelper requestHelper;

        public ResourceGuruClient(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ApiUrl = "https://api.resourceguruapp.com/";
            requestHelper = new RequestHelper(this.OAuthInfo);
            
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

        public OAuthInfo RefreshAccessToken()
        {
            var authRequest = new Dictionary<string, string>(){
                   {"refresh_token", OAuthInfo.refresh_token},
                   {"grant_type", "refresh_token"}
                };
            return Authenticate("refresh_token", authRequest);
        }

        private OAuthInfo Authenticate(string grantType, Dictionary<string, string> attributes)
        {
            attributes["client_id"] = ClientId;
            attributes["client_secret"] = ClientSecret;

            var options = new Dictionary<string, object>(){
                {"oauth_request",true}
            };

            OAuthInfo OAuthInfo = requestHelper.Post<OAuthInfo>("/oauth/token", attributes, options);
            this.OAuthInfo = OAuthInfo;

            return OAuthInfo;
        }


    }
}
