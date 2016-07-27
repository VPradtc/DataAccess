using DataAccess.Core.Model.Abstraction.Interfaces;
using System;
using AutoMapper;

namespace DataAccess.Dto.Model.Models.Base
{
    public abstract class PrimaryDto : IProfileBase, IPrimary<Guid>
    {
      public Guid Id { get; set; }

        public abstract IProfileExpression Configure(IProfileExpression config);
    }
}