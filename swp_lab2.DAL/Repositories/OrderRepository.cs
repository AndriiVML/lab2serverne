using swp_lab2.DAL.EF;
using swp_lab2.DAL.Interfaces;
using swp_lab2.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace swp_lab2.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private SageDbContext context;

        public OrderRepository(SageDbContext _context)
        {
            context = _context;
        }

        public void Create(Order order)
        {
            context.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            context.Orders.Remove(order);
        }

        public Order Get(long id)
        {
            return context.Orders.Include("Books").Where(b => b.Id == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders.Include("Books").ToList();
        }

        public void Update(Order book)
        {
            context.Entry(book).State = EntityState.Modified;
        }
    }
}
