using BookStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; } = new List<CartItem>();

        public void Add(Book book)
        {
            var item = Items.FirstOrDefault(x => x.Book.Id == book.Id);
            if(item != null)
            {
                item.Count++;
            }
            else
            {
                Items.Add(new CartItem
                {
                    Book = book,
                    Count = 1
                });
            }
        }

        public void Remove(Book book)
        {
            var item = Items.FirstOrDefault(x => x.Book.Id == book.Id);
            if(item.Count > 1)
            {
                item.Count--;
            }
            else
            {
                Items.Remove(item);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
