using CoreWebApi.src.Product.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CoreWebApi.src.Product.Repository
{
    public class ProductDao : IProductDao
    {
        private readonly string _connStr;
        public ProductDao(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProductDto> GetProducts()
        {
            var query = ExecuteCommand(_connStr,
                   conn => conn.Query<ProductDto>("spGetAllProducts")).ToList();
            return query;
        }

        public void AddProduct(ProductDto productItem)
        {
            ExecuteCommand(_connStr, conn => {
                conn.Query<ProductDto>("spAddProduct",
                    new { Name = productItem.Name, Brand = productItem.Brand }, commandType: CommandType.StoredProcedure);
            });
        }

        public void UpdateProduct(int id, ProductDto productItem)
        {
            ExecuteCommand(_connStr, conn => {
                conn.Query<ProductDto>("spUpdateProduct",
                    new { ID=id, Name = productItem.Name, Brand = productItem.Brand }, commandType: CommandType.StoredProcedure);
            });
        }

        public void DeleteProduct(int id)
        {
            ExecuteCommand(_connStr, conn => {
                conn.Query<ProductDto>("spDeleteProduct",
                    new { ID = id}, commandType: CommandType.StoredProcedure);
            });
        }

        #region Helpers

        private void ExecuteCommand(string connStr, Action<SqlConnection> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                task(conn);
            }
        }
        private T ExecuteCommand<T>(string connStr, Func<SqlConnection, T> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                return task(conn);
            }
        }

        #endregion


    }
}
