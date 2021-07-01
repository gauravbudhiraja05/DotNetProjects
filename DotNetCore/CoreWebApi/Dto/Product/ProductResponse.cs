using Dto.Common;

namespace Dto.Product
{
    public class ProductDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }

    public class ProductResponse: ListResponse<ProductDto>
    {
        
    }
}
