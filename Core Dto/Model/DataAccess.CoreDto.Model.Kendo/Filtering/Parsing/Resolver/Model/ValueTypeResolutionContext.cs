using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver.Model
{
    public class ValueTypeResolutionContext
    {
        public readonly string Value;
        public readonly Type DesiredType;

        public ValueTypeResolutionContext(string value, Type desiredType)
        {
            Value = value;
            DesiredType = desiredType;
        }
    }
}
