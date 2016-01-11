using ResourceGuru.Authentication;

namespace ResourceGuru.Utils.Authentication
{
    public interface IAuthStore
    {
        /// <summary>
        /// Get AuthInfo object from store
        /// </summary>
        /// <returns></returns>
        OAuthInfo Get();

        /// <summary>
        /// Store AuthInfoobject to store
        /// </summary>
        /// <param name="oAuthInfo"></param>
        void Set(OAuthInfo oAuthInfo);
    }
}
