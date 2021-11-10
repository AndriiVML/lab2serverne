using swp_lab2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swp_lab2.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        BookRepository Books { get; }
        SageRepository Sages { get; }
        OrderRepository Orders { get; }

        void Save();
    }

}
