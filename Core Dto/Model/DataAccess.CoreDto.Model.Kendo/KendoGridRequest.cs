using System.Collections.Generic;

namespace DataAccess.CoreDto.Model.Kendo
{
    public class KendoGridRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<KendoSortItem> Sort { get; set; }
        public KendoGridFilters Filter { get; set; }
    }
}