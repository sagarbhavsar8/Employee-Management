using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.Repositories;
using AddToMyCart.DomainModels;
using AddToMyCart.ViewModels;
using AutoMapper;

namespace AddToMyCart.ServiceLayer
{
    public interface IProductsService
    {
        IEnumerable<ProductViewModel> GetAllProducts();
        void InsertProduct(ProductViewModel product);
        IEnumerable<ProductViewModel> GetProductByID(int productID);
        IEnumerable<ProductViewModel> GetProductByName(string productName);
    }

    public class ProductsService : IProductsService
    {

        private readonly IProductsRepository pr;

        public ProductsService(IProductsRepository pr)
        {
            this.pr = pr;
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            IEnumerable<Product> products = this.pr.GetAllProducts();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            IEnumerable<ProductViewModel> p = mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(products);
            return p;
        }

        public IEnumerable<ProductViewModel> GetProductByID(int productID)
        {
            IEnumerable<Product> products = this.pr.GetProductByID(productID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            IEnumerable<ProductViewModel> p = mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return p;
        }

        public IEnumerable<ProductViewModel> GetProductByName(string productName)
        {
            IEnumerable<Product> products = this.pr.GetProductByName(productName);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            IEnumerable<ProductViewModel> p = mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return p;
        }

        public void InsertProduct(ProductViewModel product)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ProductViewModel, Product>(); cfg.IgnoreUnmapped(); });

        }
    }
}
