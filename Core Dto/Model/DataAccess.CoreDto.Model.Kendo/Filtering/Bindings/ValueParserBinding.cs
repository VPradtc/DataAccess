using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings
{
    public class ValueParserBinding
    {
        public readonly Type Type;
        public readonly IValueParser Parser;

        public ValueParserBinding(Type type, IValueParser parser)
        {
            Type = type;
            Parser = parser;
        }
    }
}
