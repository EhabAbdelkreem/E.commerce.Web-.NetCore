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
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productdb = _context.Products.FirstOrDefault( X => X.Id == product.Id);
            if (productdb != null)
            {
                productdb.Name = product.Name;
                productdb.Description = product.Description;
                productdb.Price = product.Price;
                if (productdb.ImageUrl !=null)
                {
                    productdb.ImageUrl = productdb.ImageUrl;    

                }

            }

        }
    }
}
