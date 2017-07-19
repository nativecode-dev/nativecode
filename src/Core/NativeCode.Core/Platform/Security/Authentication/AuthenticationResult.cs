namespace NativeCode.Core.Platform.Security.Authentication
{
    using System.Security.Principal;

    public class AuthenticationResult
    {
        public AuthenticationResult(AuthenticationResultType result, IPrincipal principal = null)
        {
            this.Principal = principal;
            this.Result = result;
        }

        public IPrincipal Principal { get; }

        public AuthenticationResultType Result { get; }
    }
}