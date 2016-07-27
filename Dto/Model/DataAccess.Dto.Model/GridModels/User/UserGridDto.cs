using System;
using System.Linq;
using AutoMapper;
using DataAccess.Dto.Model.Models.Base;
using DataAccess.Entities.Enums;

namespace DataAccess.Dto.Model.GridModels
{
    public class UserGridDto : PrimaryDto
    {
        #region Property

        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public RoleIdentifier RoleId { get; set; }

        #endregion Property

        public override IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Entities.Identity.User, UserGridDto>()
                 .ForMember(
                 dto => dto.RoleId,
                 entity => entity
                 .MapFrom(u => u.Roles.FirstOrDefault().Role.Identifier));

            return config;
        }
    }
}
