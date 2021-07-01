using System;
using System.Collections.Generic;
using System.Linq;

namespace SaleClassLibrary
{
    public class SaleService : ISaleService
    {
        DataAccessLayer dataAccessLayer;
        public SaleService()
        {
            dataAccessLayer = new DataAccessLayer();
        }

        public static List<Customer> cutomerList = new List<Customer>()
         {
                new Customer {CustomerID = 1, CustomerName="Sujeet",
                Address="Pune", EmailId="test@yahoo.com" },
                new Customer {CustomerID = 2, CustomerName="Rahul",
                Address="Pune", EmailId="test@yahoo.com" },
                new Customer {CustomerID = 3, CustomerName="Mayur",
                Address="Pune", EmailId="test@yahoo.com"}
        };

        public bool InsertCustomer(Customer customer)
        {
            if (dataAccessLayer.SaveData(customer) > 0)
                return true;
            else
                return false;
        }

        public List<Customer> GetAllCustomer()
        {
            return dataAccessLayer.GetAllCustomer();
        }

        public bool DeleteCustomer(int customerId)
        {
            int rowsAffected = dataAccessLayer.DeleteCustomer(customerId);

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            cutomerList.Where(p => p.CustomerID ==
            customer.CustomerID).Update(p => p.CustomerName = customer.CustomerName);
            return true;
        }
    }

    public static class LinqUpdates
    {
        public static void Update<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}
