using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.DomainModels;

namespace AddToMyCart.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts();
        void InsertProduct(Product product);
        IEnumerable<Product> GetProductByID(int productID);
        IEnumerable<Product> GetProductByName(string productName);
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly AddToMyWebCartDbContext db;

        public ProductsRepository()
        {
            db = new AddToMyWebCartDbContext();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return db.Products.ToList();
        }

        public IEnumerable<Product> GetProductByID(int productID)
        {
            return db.Products.Where(p => p.ProductID == productID).ToList();
        }

        public IEnumerable<Product> GetProductByName(string productName)
        {
            return db.Products.Where(p => p.ProductName.ToLower().Contains(productName)).ToList();
        }

        public void InsertProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
    }
}
