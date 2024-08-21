using E.Comerce.DataAccessLayer.Data;
using E.Comerce.DataAccessLayer.Infrastructer.Irepository;
using E.Comerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Comerce.DataAccessLayer.Infrastructer.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int DecrementtCartItem(Cart cart, int count)
        {
            cart.count -= count;
            return cart.count;
        }

        public int IncreamentCartItem(Cart cart, int count)
        {
            cart.count += count; 
            return cart.count;  
        }
    }
}
