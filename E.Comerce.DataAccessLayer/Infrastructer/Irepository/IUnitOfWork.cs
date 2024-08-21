using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Comerce.DataAccessLayer.Infrastructer.Irepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category{ get; }
        IProductRepository Product { get; }
        IApplicationUser applicationUser { get; }
        ICartRepository Cart { get; } 
        void save();
    }
}
