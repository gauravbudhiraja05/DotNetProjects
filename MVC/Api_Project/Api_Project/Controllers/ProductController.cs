using Api_Project.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Api_Project.Controllers
{
    public class ProductController : ApiController
    {
        [BasicAuthentication]
        [HttpGet]
        public List<Product_Table> GetProducts()
        {
            using (ProductDbEntities db = new ProductDbEntities())
            {
                return db.Product_Table.ToList();
            }
        }
    }
}
