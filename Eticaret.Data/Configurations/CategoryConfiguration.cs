using Eticaret.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eticaret.Data.Configurations
{
    internal class CategoryConfigurationv : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.İmage).HasMaxLength(50);
            builder.HasData(
                new Category
                {
                    Name = "KOZZMETİK",
                    Id=1,
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 1,
                },
                 new Category
                 {
                     Name = "GÜNDÜZ BAKIM",
                     Id = 2,
                     IsActive = true,
                     IsTopMenu = true,
                     ParentId = 0,
                     OrderNo = 2,
                 }
                );
        }
    }
}
