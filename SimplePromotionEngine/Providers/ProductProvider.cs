using SimplePromotionEngine.Contract;
using SimplePromotionEngine.Model;
using System.Collections.Generic;
using System.Linq;

namespace SimplePromotionEngine.Providers
{
    public class ProductProvider : IProduct
    {
        public Product GetProduct(char sku)
        {
            var allProducts = GetProducts();
            var product = allProducts.FirstOrDefault(x => x.Sku == sku);
            return product;
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product> {
                                new Product { Sku = 'A', Price = 50 },
                                new Product { Sku = 'B', Price = 30 },
                                new Product { Sku = 'C', Price = 20 },
                                new Product { Sku = 'D', Price = 15 }
                            };
            return products;
        }

        public PromotionRule GetPromotion(char sku)
        {
            return sku switch
            {
                'A' => new PromotionRule { PromoItems = 3, Value = 130 },
                'B' => new PromotionRule { PromoItems = 2, Value = 45 },
                _ => new PromotionRule { PromoItems = 1, Value = 0 }
            };
        }
    }
}
