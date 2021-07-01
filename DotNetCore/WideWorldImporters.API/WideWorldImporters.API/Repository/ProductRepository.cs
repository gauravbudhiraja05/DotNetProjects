using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WideWorldImporters.API.Models;
using WideWorldImporters.API.Services.Queries;

namespace WideWorldImporters.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICommandText _commandText;
        private readonly string _connStr;
        public ProductRepository(IConfiguration configuration, ICommandText commandText)
        {
            _commandText = commandText;
            // _connStr = configuration.GetConnectionString("DefaultConnection");
            _connStr = Convert.ToString(configuration["AppSettings:DefaultConnection"]);
        }


        public List<Product> GetAllProducts()
        {
            var query = ExecuteCommand(_connStr,
                   conn => conn.Query<Product>(_commandText.GetProducts)).ToList();
            return query;
        }
        public Product GetById(int id)
        {
            var product = ExecuteCommand<Product>(_connStr, conn =>
                conn.Query<Product>(_commandText.GetProductById, new { @Id = id }).SingleOrDefault());
            return product;
        }
        public void AddProduct(Product entity)
        {
            ExecuteCommand(_connStr, conn =>
            {
                var query = conn.Query<Product>(_commandText.AddProduct,
                    new { Name = entity.Name, Brand = entity.Brand, CreatedDate = entity.CreatedDate });
            });
        }
        public void UpdateProduct(Product entity, int id)
        {
            ExecuteCommand(_connStr, conn =>
            {
                var query = conn.Query<Product>(_commandText.UpdateProduct,
                    new { Name = entity.Name, Brand = entity.Brand, Id = id });
            });
        }

        public void RemoveProduct(int id)
        {
            ExecuteCommand(_connStr, conn =>
            {
                var query = conn.Query<Product>(_commandText.RemoveProduct, new { Id = id });
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
