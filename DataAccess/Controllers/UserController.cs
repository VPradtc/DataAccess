using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess.Controllers.Base;
using DataAccess.CoreDto.Model.Kendo;
using DataAccess.Dto.Model.GridModels;
using DataAccess.Dto.Model.Models;
using DataAccess.Managers.Abstraction;
using DataAccess.Model.Definition.Providers.Identity;
using DataAccess.Dto.Model.Models.User;
using AutoMapper;

namespace DataAccess.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IRoleDefinitionProvider _roleDefinitionProvider;

        #region Constructors

        public UserController(IDataBaseManager manager,
            IRoleDefinitionProvider roleDefinitionProvider) : base(manager)
        {
            _roleDefinitionProvider = roleDefinitionProvider;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<HttpResponseMessage> GetByPage([FromUri]KendoGridRequest request)
        {
            var grid = await DataBaseManager.UserRepositoryResolver.GetByPageAsync<UserGridDto>(request, "CreatedDate");

            return Request.CreateResponse(HttpStatusCode.OK, grid);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            var entity = await DataBaseManager.UserRepositoryResolver.GetByIdAsync<UserDto>(id);

            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        public async Task<HttpResponseMessage> Update(UserDto user)
        {
            await DataBaseManager.UserRepositoryResolver.UpdateAsync(user, user.RoleId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(UserDto user)
        {
            var entityUser = await DataBaseManager.UserRepositoryResolver.CreateAsync(user, user.Password, user.RoleId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            await DataBaseManager.UserRepositoryResolver.DeactivateAsync(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [OverrideAuthorization]
        public async Task<HttpResponseMessage> IsUniqueEmail([FromUri]string email)
        {
            var sanitizedEmail = email.Trim();

            var result = await DataBaseManager.UserRepositoryResolver.IsUniqueEmail(sanitizedEmail);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [OverrideAuthorization]
        public async Task<HttpResponseMessage> Register(UserRegisterDto model)
        {
            var user = Mapper.Map<UserRegisterDto, UserDto>(model);

            var entityUser = await DataBaseManager.UserRepositoryResolver.CreateAsync(user, user.Password, user.RoleId);

            return Request.CreateResponse(HttpStatusCode.OK, entityUser);
        }

        #endregion
    }
}
