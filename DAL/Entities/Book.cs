using System.Collections.Generic;

namespace BookStore.DAL.Entities
{
    public class Book
    {
        public Book()
        {
            BookSages = new List<BookSage>();
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookSage> BookSages { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
