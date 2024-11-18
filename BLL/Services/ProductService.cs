using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService
    {
        private ProductRepo _repo = new ProductRepo();

        public List<Product> GetAll() => _repo.GetAll();
        
        public void CreateProduct(Product product)  => _repo.Create(product);

        public void UpdateProduct(Product product) => _repo.Update(product);

        public void DeleteProduct(Product product) => _repo.Delete(product);
    }
}
