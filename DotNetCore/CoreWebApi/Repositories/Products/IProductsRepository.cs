using Domains.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Products
{
    interface IProductsRepository
    {
        List<Product> GetProducts();

        Product AddProduct(Product productItem);

        Product UpdateProduct(string id, Product productItem);

        string DeleteProduct(string id);
    }
}
