namespace BookStore.DAL.Entities
{
    public class BookSage
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int SageId { get; set; }
        public Sage Sage { get; set; }
    }
}
