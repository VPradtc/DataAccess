using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory
{
    public class StringValueParser : IValueParser
    {
        public object Parse(string input)
        {
            return input;
        }
    }
}
