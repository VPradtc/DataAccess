using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory
{
    public class DateTimeValueParser : IValueParser
    {
        public object Parse(string input)
        {
            return DateTime.Parse(input).Date;
        }
    }
}
