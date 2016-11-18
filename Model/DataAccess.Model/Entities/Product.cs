using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Entities.BaseEntities;
using DataAccess.Entities.Identity;
using System;
using System.Data.Entity;

namespace DataAccess.Entities.Entities
{
    public class Product : PrimaryEntity, IEntityMap
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId);
        }
    }
}
