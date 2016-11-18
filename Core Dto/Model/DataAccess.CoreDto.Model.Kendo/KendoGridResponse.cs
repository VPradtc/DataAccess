using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.CoreDto.Model.Kendo
{
    public class KendoGridResponse<TDto>
    {
        public int Total { get; set; }
        public IEnumerable<TDto> Data { get; set; }

        public async static Task<KendoGridResponse<TDto>> GenerateResponseAsync(IQueryable<TDto> records, int page, int pageSize)
        {
            var result = new KendoGridResponse<TDto>
            {
                Data = records.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Total = records.Count()
            };

            return result;
        }
    }
}