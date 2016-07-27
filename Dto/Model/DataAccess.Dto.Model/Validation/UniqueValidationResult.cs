using AutoMapper;
using DataAccess.Dto.Model.Models.Base;

namespace DataAccess.Dto.Model.Validation
{
    public class UniqueValidationResult : IProfileBase
    {
        public bool IsUnique { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<bool, UniqueValidationResult>()
                .ForMember(dest => dest.IsUnique,
                src => src.MapFrom(value => value)
                );

            return config;
        }
    }
}
