using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DAL.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasOne(o => o.Book)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BookId);

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();
        }
    }
}
