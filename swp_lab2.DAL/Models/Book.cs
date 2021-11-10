using swp_lab2.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.Models
{
    public class Book : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Sage> Sages { get; set; }

        public virtual List<Order> Orders { get; set; }

        public Book()
        {
            Sages = new List<Sage>();
            Orders = new List<Order>();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Book;

            if (item == null)
                return false;

            return this.Id == item.Id;
        }
    }
}
