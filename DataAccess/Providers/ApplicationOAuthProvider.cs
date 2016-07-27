using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using DataAccess.Core.Dal.Implementation.Repositories;
using DataAccess.Entities.Identity;
using DataAccess.IdentityTokenCore.Authentication.Providers;
using DataAccess.Dal.Identity;
using DataAccess.Dal.Abstraction.Interfaces;
using Ninject;
using DataAccess.IdentityTokenCore.Authentication.Abstraction.Properties_Formater;
using DataAccess.Dal.Repositories;
using DataAccess.ApplicationLogger.Abstraction.Interfaces;

namespace DataAccess.Providers
{
    public class ApplicationOAuthProvider : IdentityTokenOAuthProvider
    {
        private IAuthClientRepository _authClientRepository;

        #region Construtor

        public ApplicationOAuthProvider(
            IIdentityTokenPropertiesFormater<User, Guid> propertiesFormatter)
            : base(propertiesFormatter, "DataAccessClientId")
        {
            _authClientRepository = new AuthClientRepository(_context);
        }

        #endregion Construtor

        #region Override

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await base.ValidateClientAuthentication(context);

            // Resource owner password credentials does not provide a client ID.
            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context
                //if you want to force sending clientId/secrects once obtain access tokens.
                //context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");

                return;
            }


            AuthClient client = await _authClientRepository.Get(t => t.Name == context.ClientId).FirstOrDefaultAsync();

            if (client == null)
            {
                context.SetError("invalid_clientId", $"Client '{context.ClientId}' is not registered in the system.");

                return;
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return;
            }

            context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime);

            context.Validated();
        }

        #endregion Override
    }
}