using DataAccess.Core.Extensions.System;

namespace DataAccess.CoreDto.Model.Kendo
{
    public class KendoFilterItem
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }

        public string FormattedField => Field.ToTitleCase();
    }
}