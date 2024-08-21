using E.Comerce.DataAccessLayer.Data;
using E.Comerce.DataAccessLayer.Infrastructer.Irepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Comerce.DataAccessLayer.Infrastructer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public IApplicationUser applicationUser { get; private set; }


       
        public ICartRepository Cart { get; private set; }

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context); 
            Cart = new CartRepository(context);
            applicationUser = new  ApplicationRepository(context);
        }

        

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
