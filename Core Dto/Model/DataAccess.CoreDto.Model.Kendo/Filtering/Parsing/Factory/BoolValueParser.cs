using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory
{
    public class BoolValueParser : IValueParser
    {
        public object Parse(string input)
        {
            return Boolean.Parse(input);
        }
    }
}
