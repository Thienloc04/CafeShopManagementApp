using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private UserRepo _repo = new();

        public User? Login(string username)
        => _repo.GetOne(username);

        public List<User> GetAllUser() => _repo.GetAll();

        public void CreateUser(User user) => _repo.Create(user);

        public void UpdateUser(User user) => _repo.Update(user);

        public void DeleteUser(User user) => _repo.Delete(user);    
    }
}
