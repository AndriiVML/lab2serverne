using swp_lab2.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.Models
{
    public class Sage : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public byte[] Photo { get; set; }

        public string City { get; set; }

        public virtual List<Book> Books { get; set; }

        public Sage()
        {
            Books = new List<Book>();
            Photo = new byte[0];
        }

        public override bool Equals(object obj)
        {
            var item = obj as Sage;

            if (item == null)
                return false;

            return this.Id == item.Id;
        }
    }
}
