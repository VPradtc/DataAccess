using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory
{
    public class EnumValueParser<T> : IValueParser
    {
        public object Parse(string input)
        {
            var enumType = typeof(T);

            var value = Int32.Parse(input);

            return (T)Enum.ToObject(enumType, value);
        }
    }
}
