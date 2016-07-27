using AutoMapper;
using DataAccess.Dto.Model.Models.Base;

namespace DataAccess.Dto.Model.Models
{
    public class DropDownItemDto : PrimaryDto
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        public override IProfileExpression Configure(IProfileExpression config)
        {
            return config;
        }
    }
}
