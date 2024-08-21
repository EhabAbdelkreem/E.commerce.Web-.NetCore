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
    public class ApplicationRepository : Repository<ApplicationUser>,IApplicationUser
    {
        private readonly ApplicationDbContext _context;
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
