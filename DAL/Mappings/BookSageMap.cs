using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DAL.Mappings
{
    public class BookSageMap : IEntityTypeConfiguration<BookSage>
    {
        public void Configure(EntityTypeBuilder<BookSage> builder)
        {
            builder.HasKey(bs => new { bs.BookId, bs.SageId });

            builder.HasOne(bs => bs.Book)
                .WithMany(b => b.BookSages)
                .HasForeignKey(bs => bs.BookId);

            builder.HasOne(bs => bs.Sage)
                .WithMany(c => c.BookSages)
                .HasForeignKey(bs => bs.SageId);
        }
    }
}
