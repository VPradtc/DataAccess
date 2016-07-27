using DataAccess.Entities.Identity;
using DataAccess.Model.Context;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.IdentityTokenCore.Authentication.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly DataAccessContext _context;

        public RefreshTokenProvider()
        {
            _context = new DataAccessContext();
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            var task = CreateAsync(context);
            task.Wait();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext tokenContext)
        {
            string clientName = tokenContext.Ticket.Properties.Dictionary["as:client_id"];

            if (clientName == null)
            {
                return;
            }

            Guid clientid = _context.AuthClients.First(client => client.Name == clientName).Id;

            var refreshTokenId = Guid.NewGuid();

            var refreshTokenLifeTime = tokenContext.OwinContext.Get<int>("as:clientRefreshTokenLifeTime");

            var issuedUtc = DateTime.UtcNow;
            var expiresUtc = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime);

            var token = new RefreshToken()
            {
                Id = refreshTokenId,
                ClientId = clientid,
                Subject = tokenContext.Ticket.Identity.Name,
                IssuedUtc = issuedUtc.ToString(),
                ExpiresUtc = expiresUtc.ToString()
            };

            tokenContext.Ticket.Properties.ExpiresUtc = expiresUtc;

            token.ProtectedTicket = tokenContext.SerializeTicket();

            _context.RefreshTokens.Add(token);

            var result = await _context.SaveChangesAsync();

            if (result != 0)
            {
                tokenContext.SetToken(refreshTokenId.ToString());
            }
        }

        public void Receive(AuthenticationTokenReceiveContext tokenContext)
        {
            var task = ReceiveAsync(tokenContext);
            task.Wait();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext tokenContext)
        {
            var allowedOrigin = tokenContext.OwinContext.Get<string>("as:clientAllowedOrigin");
            tokenContext.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var tokenId = Guid.Parse(tokenContext.Token);

            var refreshToken = _context.RefreshTokens.FirstOrDefault(token => token.Id == tokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                tokenContext.DeserializeTicket(refreshToken.ProtectedTicket);
                _context.RefreshTokens.Remove(refreshToken);

                var result = await _context.SaveChangesAsync();
            }
        }
    }
}
