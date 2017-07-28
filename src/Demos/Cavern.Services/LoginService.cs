﻿namespace Cavern.Services
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Data.Security;
    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Platform.Security;
    using NativeCode.Core.Platform.Security.Authentication;
    using ApplicationIdentity = NativeCode.Core.Platform.Security.ApplicationIdentity;

    [Dependency(typeof(ILoginService))]
    public class LoginService : ScraperContextService<Login>, ILoginService
    {
        private readonly ICryptographer cryptographer;

        public LoginService(ScraperContext context, ICryptographer cryptographer) : base(context)
        {
            this.cryptographer = cryptographer;
            this.EnsureDisposed(this.cryptographer);
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password,
            CancellationToken cancellationToken)
        {
            var user = await this.DataContext.Users.Include(u => u.Login).FirstOrDefaultAsync(
                u => string.Equals(u.Login.Email,
                    username, StringComparison.CurrentCultureIgnoreCase), cancellationToken);

            if (user == null)
                return new AuthenticationResult(AuthenticationResultType.NotFound);

            AuthenticationResult result;

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (user.Login.LoginType == LoginType.ActiveDirectory)
                result = new AuthenticationResult(AuthenticationResultType.Authenticated);
            else
                result = this.ValidatePassword(username, password, user);

            AddLoginAttempt(user.Login, result);

            await this.DataContext.SaveChangesAsync(cancellationToken);

            return result;
        }

        private static void AddLoginAttempt(Login login, AuthenticationResult result)
        {
            var attempt = new LoginHistory
            {
                AuthenticationResult = result.Result,
                Login = login
            };

            login.Logins.Add(attempt);
        }

        private AuthenticationResult ValidatePassword(string username, string password, User user)
        {
            var cipher = this.cryptographer.Decrypt(Encoding.UTF8.GetBytes(password), user.Login.SaltHash);

            AuthenticationResult result;

            if (cipher.SequenceEqual(user.Login.PasswordHash))
            {
                var identity = new ApplicationIdentity(username, "password");
                var principal = new ApplicationPrincipal(identity);
                result = new AuthenticationResult(AuthenticationResultType.Authenticated, principal);
            }
            else
            {
                result = new AuthenticationResult(AuthenticationResultType.Failed);
            }

            return result;
        }
    }
}