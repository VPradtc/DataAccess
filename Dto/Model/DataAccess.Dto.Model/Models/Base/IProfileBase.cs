using AutoMapper;

namespace DataAccess.Dto.Model.Models.Base
{
    public interface IProfileBase
    {
        IProfileExpression Configure(IProfileExpression config);
    }
}
