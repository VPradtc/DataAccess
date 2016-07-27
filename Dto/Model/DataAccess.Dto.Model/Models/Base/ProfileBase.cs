using AutoMapper;

namespace DataAccess.Dto.Model.Models.Base
{
    public abstract class ProfileBase
    {
        public abstract IProfileExpression Configure(IProfileExpression config);
    }
}
