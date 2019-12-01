using BookStore.DAL.Entities;

namespace MVC.Models
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Count { get; set; }
    }
}
