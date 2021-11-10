using swp_lab2.DAL.EF;
using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private SageDbContext context;

        public BookRepository(SageDbContext _context)
        {
            context = _context;
        }

        public void Create(Book book)
        {
            context.Books.Add(book);
        }

        public void Delete(Book book)
        {
            context.Books.Remove(book);
        }

        public Book Get(long id)
        {
            return context.Books.Include("Sages").Where(b => b.Id == id).FirstOrDefault();
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books.Include("Sages").ToList();
        }

        public void Update(Book book)
        {
            var bookDb = Get(book.Id);

            if (bookDb == null)
                return;

            bookDb.Name = book.Name;
            bookDb.Description = book.Description;
            bookDb.Sages = book.Sages;

            context.Entry(bookDb).State = EntityState.Modified;
        }
    }
}
