using DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver.Model;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver
{
    public interface IValueTypeResolver
    {
        object Resolve(ValueTypeResolutionContext context);
    }
}
