using AutoMapper;

namespace DataAccess.CoreDto.Model.AutoMapper
{
    public interface IMap
    {
        IProfileExpression Configure(IProfileExpression config);
    }
}
