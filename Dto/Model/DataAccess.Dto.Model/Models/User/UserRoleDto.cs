using System;
using AutoMapper;
using DataAccess.Dto.Model.Models.Base;

namespace DataAccess.Dto.Model.Models
{
    public class UserRoleDto : PrimaryDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public override IProfileExpression Configure(IProfileExpression config)
        {
            return config;
        }
    }
}
