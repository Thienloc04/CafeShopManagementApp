using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService
    {
        private RoleRepo _repo = new RoleRepo();

        public List<Role> GetAllRoles() => _repo.GetAll();
    }
}
