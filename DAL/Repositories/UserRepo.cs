using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepo
    {
        private CafeManagementSystemContext _context;

        public User? GetOne(string username)
        {
            _context = new CafeManagementSystemContext();
            return _context.Users.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        }

        public List<User> GetAll()
        {
            _context = new CafeManagementSystemContext();
            return _context.Users.Include("Role").ToList();
        } 

        public void Create(User user)
        {
            _context = new();
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context = new();
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context = new();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public int GetAllCustomers()
        {
            _context = new CafeManagementSystemContext();
            return _context.Users.Where(x => x.RoleId == 3).Count();
        }
    }
}
