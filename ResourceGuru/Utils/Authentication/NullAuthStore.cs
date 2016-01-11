using ResourceGuru.Authentication;

namespace ResourceGuru.Utils.Authentication
{
    class NullAuthStore : IAuthStore
    {
        public ResourceGuru.Authentication.OAuthInfo Get()
        {
            return new OAuthInfo();
        }

        public void Set(ResourceGuru.Authentication.OAuthInfo oAuthInfo)
        {
        }
    }
}
