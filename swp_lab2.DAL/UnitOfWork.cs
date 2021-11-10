using swp_lab2.DAL.EF;
using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SageDbContext db = new SageDbContext();
        private SageRepository sageRepository;
        private BookRepository bookRepository;
        private OrderRepository orderRepository;

        public BookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public SageRepository Sages
        {
            get
            {
                if (sageRepository == null)
                    sageRepository = new SageRepository(db);
                return sageRepository;
            }
        }

        public OrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
