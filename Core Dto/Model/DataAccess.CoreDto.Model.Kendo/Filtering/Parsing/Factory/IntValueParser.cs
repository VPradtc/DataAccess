using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory
{
    public class IntValueParser : IValueParser
    {
        public object Parse(string input)
        {
            return Int32.Parse(input);
        }
    }
}
