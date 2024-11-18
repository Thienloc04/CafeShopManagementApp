using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepo
    {
        private CafeManagementSystemContext _context;

        public List<Role> GetAll()
        {
            _context = new CafeManagementSystemContext();
            return _context.Roles.ToList();
        }
    }
}
