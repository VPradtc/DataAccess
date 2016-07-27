using DataAccess.Dto.Model.Models.Base;
using AutoMapper;
using DataAccess.Entities.Enums;

namespace DataAccess.Dto.Model.Models.User
{
    public class UserRegisterDto : TrackableDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public override IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<UserRegisterDto, UserDto>()
                .ForMember(dst => dst.RoleId,
                    options => options.UseValue<RoleIdentifier>(RoleIdentifier.ClientRetail));

            return config;
        }
    }
}
