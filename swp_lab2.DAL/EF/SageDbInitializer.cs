using swp_lab2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.EF
{
    class SageDbInitializer : DropCreateDatabaseIfModelChanges<SageDbContext>
    {
        protected override void Seed(SageDbContext context)
        {
            var sage1 = new Sage
            {
                Name = "Sage One",
                Age = 94,
                City = "Lviv",
            };

            var sage2 = new Sage
            {
                Name = "Sage Two",
                Age = 86,
                City = "Kyiv"
            };

            var book1 = new Book
            {
                Name = "Book of One Sage",
                Description = "Book of One Sage Description"
            };

            var book2 = new Book
            {
                Name = "Book of Two Sages",
                Description = "Book of Two Sages Description",
            };

            sage1.Books.Add(book1);
            sage1.Books.Add(book2);

            sage2.Books.Add(book2);

            context.Sages.Add(sage1);
            context.Sages.Add(sage2);
            context.Books.Add(book1);
            context.Books.Add(book2);

            context.SaveChanges();
        }
    }
}
