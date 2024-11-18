using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepo
    {
        private CafeManagementSystemContext _context;

        public List<Product> GetAll()
        {
            _context = new CafeManagementSystemContext();
            return _context.Products.Include("Category").ToList();
        }

        public void Create(Product obj)
        {
            _context = new();
            _context.Products.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Product obj)
        {
            _context = new();
            _context.Products.Update(obj);
            _context.SaveChanges();
        }

        public void Delete(Product obj)
        {
            _context = new();
            _context.Products.Remove(obj);
            _context.SaveChanges();
        }

        public int GetAllProducts()
        {
            _context = new();
            return _context.Products.Where(x => x.Status == 1).Count();
        }
    }
}
