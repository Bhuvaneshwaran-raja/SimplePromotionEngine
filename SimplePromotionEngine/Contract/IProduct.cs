using SimplePromotionEngine.Model;
using System.Collections.Generic;

namespace SimplePromotionEngine.Contract
{
    public interface IProduct
    {
        /// <summary>
        /// Returns all the products from the inventory
        /// </summary>
        /// <returns>Product list</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Returns products based off the SKU filter
        /// </summary>
        /// <param name="sku"></param>
        /// <returns>Produt model</returns>
        Product GetProduct(char sku);

        /// <summary>
        /// Provides promotion product count for given product sku
        /// </summary>
        /// <param name="sku"></param>
        /// <returns>PromotionRule model</returns>
        PromotionRule GetPromotion(char sku);
    }
}
