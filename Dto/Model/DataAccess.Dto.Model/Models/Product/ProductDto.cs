using AutoMapper;
using DataAccess.Dto.Model.Models.Base;
using DataAccess.Entities.Entities;

namespace DataAccess.Dto.Model.Models
{
    public class ProductDto : TrackableDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Product, ProductDto>();
            config.CreateMap<ProductDto, Product>();

            return config;
        }
    }
}
