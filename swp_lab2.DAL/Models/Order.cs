using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.Models
{
    public class Order
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public virtual List<Book> Books { get; set; }

        public Order()
        {
            Books = new List<Book>();
        }
    }
}
