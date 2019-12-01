using System.Collections.Generic;

namespace BookStore.DAL.Entities
{
    public class Sage
    {
        public Sage()
        {
            BookSages = new List<BookSage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<BookSage> BookSages { get; set; }
    }
}
