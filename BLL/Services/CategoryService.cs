using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService
    {
        private CategoryRepo _repo = new();

        public List<Category> GetCategories() => _repo.GetAll();
    }
}
