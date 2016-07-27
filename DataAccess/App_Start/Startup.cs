using System.Web.Http;
using Microsoft.Owin;
using Owin;
using DataAccess;
using DataAccess.CoreDto.Model.AutoMapper;
using DataAccess.Filters;
using DataAccess.IdentityTokenCore.Authentication;
using DataAccess.Providers;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using DataAccess.Injector;
using System.Data.Entity;
using DataAccess.IdentityTokenCore.Authentication.Providers;
using Newtonsoft.Json.Serialization;
using DataAccess.Dal.Identity;
using DataAccess.Model.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccess.Entities.Identity;
using System;
using Microsoft.Owin.Security.DataProtection;

[assembly: OwinStartup(typeof(Startup))]
namespace DataAccess
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);
            ApplicationUserManager.SetDataProtectionProvider(app.GetDataProtectionProvider());

            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => new DataAccessContext());
            app.CreatePerOwinContext(() =>
            {
                var context = new DataAccessContext();
                var userStore = new UserStore<User, Role, Guid, UserLogin, UserRole, Claim>(context);

                return new ApplicationUserManager(userStore);
            });

            //Authentication configuration
            var authConfig = NinjectHttpContainer.Resolve<AuthenticationConfiguration>();

            authConfig.Config(accessTokenExpireMinutes: 120);
            authConfig.ConfigureAuth(app);

            var config = new HttpConfiguration();

            ////WebApi configuration
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // NInject config
            app.UseNinjectMiddleware(NinjectHttpContainer.GetKernel);
            app.UseNinjectWebApi(config);

            //Auto Maper
            AutoMapperStartup.Register(NinjectHttpContainer.GetKernel().GetService);

            //Logger
            ApplicationLogger.Logger.Configure();
            config.MessageHandlers.Add(NinjectHttpContainer.Resolve<DelegatingLogFilter>());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}