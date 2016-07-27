using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using DataAccess.IdentityTokenCore.Authentication.Abstraction.Properties_Formater;
using Ninject;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Dal.Identity;
using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;
using DataAccess.Model.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.IdentityTokenCore.Authentication.Providers
{
    public class IdentityTokenOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        protected readonly DataAccessContext _context;
        private readonly IIdentityTokenPropertiesFormater<User, Guid> _propertiesFormatter;

        protected ApplicationUserManager _userManager;

        #region Constructors

        /// <exception cref="ArgumentNullException">Throw when publicClientId is null or empty</exception>
        public IdentityTokenOAuthProvider(
            IIdentityTokenPropertiesFormater<User, Guid> propertiesFormatter,
            string publicClientId)
        {
            if (string.IsNullOrEmpty(publicClientId))
            {
                throw new ArgumentNullException("publicClientId");
            }

            _context = new DataAccessContext();
            _publicClientId = publicClientId;
            _propertiesFormatter = propertiesFormatter;

            var userStore = new UserStore<User, Role, Guid, UserLogin, UserRole, Claim>(_context);
            _userManager = new ApplicationUserManager(userStore);
        }

        #endregion

        #region Public Methods

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            return base.MatchEndpoint(context);
        }

        /// <exception cref="ArgumentNullException">Throw exception when user manager not set in OwinContext</exception>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";

            context.OwinContext.Response.Headers.AppendValues("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var user = await _userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var oAuthIdentity = await _userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);

            var properties = _propertiesFormatter.CreateProperties(user);

            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            ticket.Properties.Dictionary.Add("as:client_id", context.ClientId ?? string.Empty);

            context.Validated(ticket);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        #endregion Public Methods
    }
}
