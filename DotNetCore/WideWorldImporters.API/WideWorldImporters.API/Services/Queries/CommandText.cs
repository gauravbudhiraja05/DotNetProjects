using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WideWorldImporters.API.Services.Queries
{
    public interface ICommandText
    {
        string GetProducts { get; }
        string GetProductById { get; }
        string AddProduct { get; }
        string UpdateProduct { get; }
        string RemoveProduct { get; }
    }
    public class CommandText : ICommandText
    {
        public string GetProducts => "Select * From Product";
        public string GetProductById => "Select * From Product Where Id= @Id";
        public string AddProduct => "Insert Into  Product (Name, Brand, CreatedDate) Values (@Name, @Brand, GETDATE())";
        public string UpdateProduct => "Update Product set Name = @Name, Brand = @Brand, CreatedDate = GETDATE() Where Id =@Id";
        public string RemoveProduct => "Delete From Product Where Id= @Id";
    }
}
