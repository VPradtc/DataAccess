using System;
using System.Linq;
using AutoMapper;
using DataAccess.Dto.Model.Models.Base;
using DataAccess.Entities.Enums;

using DomainModel = DataAccess.Entities.Identity.User;

namespace DataAccess.Dto.Model.Models
{
    public class UserDto : PrimaryDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public RoleIdentifier RoleId { get; set; }

        public override IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<DomainModel, UserDto>()
                .ForMember(dto => dto.RoleId, entity => entity.MapFrom(u => u.Roles.FirstOrDefault().Role.Identifier));
            config.CreateMap<UserDto, DomainModel>();

            return config;
        }
    }
}
