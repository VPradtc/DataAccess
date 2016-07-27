using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Entities.Entities.BaseEntities
{
    public class PrimaryEntity : IPrimary<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual Guid Id { get; set; }
    }
}
