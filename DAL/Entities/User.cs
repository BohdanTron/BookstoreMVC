using System.Collections.Generic;

namespace BookStore.DAL.Entities
{
    public class User
    {
        public User()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
