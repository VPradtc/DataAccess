using DataAccess.ApplicationLogger;
using DataAccess.ApplicationLogger.Abstraction.Interfaces;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Dal.Identity;
using DataAccess.Dal.Repositories;
using DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces;
using DataAccess.Dto.Dal.Implementation.Repositories;
using DataAccess.Entities.Identity;
using DataAccess.Managers;
using DataAccess.Managers.Abstraction;
using DataAccess.Model.Context;
using DataAccess.Model.Definition.Providers.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using System;
using System.Data.Entity;
using Ninject.Web.Common;
using DataAccess.IdentityTokenCore.Authentication;
using DataAccess.IdentityTokenCore.Authentication.Providers;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using DataAccess.Providers;
using Ninject;
using DataAccess.IdentityTokenCore.Authentication.Abstraction.Properties_Formater;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.ServiceLocator;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Implementation;
using DataAccess.CoreDto.Model.Kendo.Filtering;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy;
using DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Decorator;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Filtering.Services.ValueExpresssion;
using DataAccess.CoreDto.Model.Kendo.Filtering.Services.OperatorExpression;
using DataAccess.CoreDto.Model.Kendo.Filtering.Services.FilterUnionExpression;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Service;

namespace DataAccess.Injector
{
    public class NinjectHttpModules
    {
        public static NinjectModule[] Modules => new NinjectModule[] { new MainModule(), new DebugModule() };

        public class MainModule : NinjectModule
        {
            public override void Load()
            {
                Kernel.Bind<DbContext, DataAccessContext>().ToMethod(activationContext => DataAccessContext.CreateContext()).InRequestScope();
                Kernel.Bind<IDataBaseManager>().To<DataBaseManager>();
                Kernel.Bind<ApplicationUserManager>().ToSelf();

                Kernel.Bind<AuthenticationConfiguration>().ToSelf();
                Kernel.Bind<IOAuthAuthorizationServerProvider>().To<ApplicationOAuthProvider>();
                Kernel.Bind<IAuthenticationTokenProvider>().To<RefreshTokenProvider>();

                Kernel.Bind<IIdentityTokenPropertiesFormater<User, Guid>>().To<ApplicationPropertiesFormater>();

                Kernel.Bind<IUserStore<User, Guid>>().To<UserStore<User, Role, Guid, UserLogin, UserRole, Claim>>();

                Kernel.Bind<IRoleDefinitionProvider>().To<RoleDefinitionProvider>();

                Kernel.Bind<IAuthClientRepository>().To<AuthClientRepository>();

                Kernel.Bind<IUserRepositoryResolver>().To<UserRepositoryResolver>();
                Kernel.Bind<IUserRepository>().To<AdoNetUserRepository>();

                Kernel.Bind<IRoleRepositoryResolver>().To<RoleRepositoryResolver>();
                Kernel.Bind<IRoleRepository>().To<RoleRepository>();

                Kernel.Bind<IApplicationLogger>().ToMethod(context =>
                {
                    var targetType = context.Request.Target.Type;
                    return new Logger(targetType);
                });

                #region Kendo Grid

                #region Sorting

                Kernel.Bind(typeof(IDynamicSortingService<>)).To(typeof(DynamicSortingService<>));
                Kernel.Bind(typeof(IPropertyOrderExpressionBuilderLocator<>)).To(typeof(PropertyOrderExpressionBuilderLocator<>));
                Kernel.Bind(typeof(IPlainPropertyOrderExpressionBuilder<>)).To(typeof(PlainPropertyOrderExpressionBuilder<>));

                #endregion

                #region Filtering

                Kernel.Bind(typeof(IFilterExpressionBuilder<>)).To(typeof(FilterExpressionBuilder<>));

                Kernel.Bind<IOwnPropertyAccessStrategy>().To<OwnPropertyAccessStrategy>();
                Kernel.Bind<INavigationPropertyAccessStrategy>().To<NavigationPropertyAccessStrategy>();

                Kernel.Bind<IPropertyAccessDecorator>().To<DateTimePropertyAccessDecorator>();

                Kernel.Bind<IPropertyAccessStrategyChainBuilder>().To<PropertyAccessStrategyChainBuilder>();
                Kernel.Bind<IValueTypeResolver>().To<EnumValueTypeResolver>();

                Kernel.Bind<IPrimitiveTypeOperatorBindingFactory>().To<PrimitiveTypeOperatorBindingFactory>();
                Kernel.Bind<IStringOperatorBindingFactory>().To<StringOperatorBindingFactory>();

                Kernel.Bind<IFilterUnionOperatorBindingFactory>().To<FilterUnionOperatorBindingFactory>();

                Kernel.Bind<IValueExpressionService>().To<ValueExpressionService>();
                Kernel.Bind<IOperatorExpressionFactoryService>().To<OperatorExpressionFactoryService>();
                Kernel.Bind<IFilterUnionExpressionFactoryService>().To<FilterUnionExpressionFactoryService>();

                #endregion

                #endregion
            }
        }

        public class DebugModule : NinjectModule
        {
            public override void Load()
            {
            }
        }

        public class ReleaseModule : NinjectModule
        {
            public override void Load()
            {
            }
        }
    }
}