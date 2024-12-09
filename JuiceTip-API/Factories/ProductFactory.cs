using JuiceTip_API.Data;
using JuiceTip_API.Model;

namespace JuiceTip_API.Factories
{
    public class ProductFactory
    {
        public MsProduct CreateProduct(ProductRequest product)
        {
            return new MsProduct
            {
                ProductId = product.ProductId,
                ProductImage = product.ProductImage,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = Math.Round(product.ProductPrice, 7),
                CategoryId = product.CategoryId,
                CustomerId = product.CustomerId,
                RegionId = product.RegionId,
                Notes = product.Notes,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now
            };
        }

    }
}
