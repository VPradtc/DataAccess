using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory
{
    public class DecimalValueParser : IValueParser
    {
        public object Parse(string input)
        {
            return Decimal.Parse(input);
        }
    }
}
