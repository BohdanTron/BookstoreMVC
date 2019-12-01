using BookStore.DAL.Mappings;
using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Sage> Sages { get; set; }
        public DbSet<BookSage> BookSages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new SageMap());
            modelBuilder.ApplyConfiguration(new BookSageMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
        }
    }
}
