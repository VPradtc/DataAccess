using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace DataAccess.IdentityTokenCore.Authentication
{
    public class AuthenticationConfiguration
    {
        private readonly IOAuthAuthorizationServerProvider _accessTokenProvider;
        private readonly IAuthenticationTokenProvider _refreshTokenProvider;

        #region Fields

        public OAuthBearerAuthenticationOptions OAuthBearerOptions { get; set; }
        public OAuthAuthorizationServerOptions OAuthOptions { get; set; }
        public string TokenEndpointPath { get; set; }
        public int AccessTokenExpireMinutes { get; set; }
        public bool AllowInsecureHttp { get; set; }

        #endregion

        public AuthenticationConfiguration(
            IOAuthAuthorizationServerProvider accessTokenProvider,
            IAuthenticationTokenProvider refreshTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
        }

        private OAuthAuthorizationServerOptions CreateOptions()
        {
            return new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(TokenEndpointPath),
                Provider = _accessTokenProvider,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(AccessTokenExpireMinutes),
                AllowInsecureHttp = AllowInsecureHttp,
                RefreshTokenProvider =_refreshTokenProvider,
            };
        }

        public void Config(string tokenEndpointPath = "/Token",
            int accessTokenExpireMinutes = 1, bool allowInsecureHttp = true)
        {
            TokenEndpointPath = tokenEndpointPath;
            AccessTokenExpireMinutes = accessTokenExpireMinutes;
            AllowInsecureHttp = allowInsecureHttp;
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            // Configure the application for OAuth based flow
            OAuthOptions = CreateOptions();

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }

    }
}
