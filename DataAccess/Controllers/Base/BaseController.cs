using System.Web.Http;
using DataAccess.Managers.Abstraction;
using DataAccess.Filters;

namespace DataAccess.Controllers.Base
{
    [InternalServerErrorExceptionFilter]
    public class BaseController : ApiController
    {
        protected IDataBaseManager DataBaseManager;

        public BaseController(IDataBaseManager manager)
        {
            DataBaseManager = manager;
        }
    }
}
