using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DAL.Mappings
{
    public class SageMap : IEntityTypeConfiguration<Sage>
    {
        public void Configure(EntityTypeBuilder<Sage> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
