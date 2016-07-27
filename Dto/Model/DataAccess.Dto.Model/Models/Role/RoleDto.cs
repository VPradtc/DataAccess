using AutoMapper;
using DataAccess.Dto.Model.Models.Base;
using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;

namespace DataAccess.Dto.Model.Models
{
    public class RoleDto : PrimaryDto
    {
        public string Name { get; set; }
        public RoleIdentifier Identifier { get; set; }

        public override IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Role, RoleDto>()
                .ForMember(dto => dto.Name, entity => entity.MapFrom(r => r.Name))
                .ForMember(dto => dto.Identifier, entity => entity.MapFrom(r => r.Identifier));

            return config;
        }
    }
}
