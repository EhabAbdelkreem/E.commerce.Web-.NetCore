using E.Comerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Comerce.DataAccessLayer.Infrastructer.Irepository
{
   public interface ICartRepository:IRepository<Cart>
    {
        int IncreamentCartItem(Cart cart ,  int count);
        int DecrementtCartItem(Cart cart, int count);

    }
}
