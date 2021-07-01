using Domains.Product;
using System.Collections.Generic;

namespace Services.Products
{
    interface IProductsService
    {
        List<Product> GetProducts();

        Product AddProduct(Product productItem);

        Product UpdateProduct(string id, Product productItem);

        string DeleteProduct(string id);
    }
}
