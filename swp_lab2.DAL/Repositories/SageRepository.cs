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
    public class SageRepository : IRepository<Sage>
    {
        private SageDbContext context;

        public SageRepository(SageDbContext _context)
        {
            context = _context;
        }

        public void Create(Sage sage)
        {
            context.Sages.Add(sage);
        }

        public void Delete(Sage sage)
        {
            context.Sages.Remove(sage);
        }

        public Sage Get(long id)
        {
            return context.Sages.Include("books").Where(b => b.Id == id).FirstOrDefault();
        }

        public IEnumerable<Sage> GetAll()
        {
            return context.Sages.Include("Books").ToList();
        }

        public void Update(Sage sage)
        {
            //var sageDb = Get(sage.Id);

            //if (sageDb == null)
            //    return;

            //sageDb.Name = sage.Name;
            //sageDb.City = sage.City;
            //sageDb.Photo = sage.Photo;
            //sageDb.Books = sage.Books;

            context.Entry(sage).State = EntityState.Modified;
        }
    }
}
