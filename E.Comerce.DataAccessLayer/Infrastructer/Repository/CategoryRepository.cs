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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var Categorydb = _context.Categories.FirstOrDefault( x => x.Id ==category.Id);  
            if (Categorydb != null) { 
                Categorydb.Name = category.Name;    
                Categorydb.DiplayOrder = category.DiplayOrder;  
            }
        }
    }
}
