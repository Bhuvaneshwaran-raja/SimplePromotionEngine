using System.Collections.Generic;
using System.Linq;

namespace SimplePromotionEngine.Extensions
{
    public static class ProductExtension
    {
        public static bool IsValidProduct(this List<char> skus, List<Model.Product> products)
        {
            bool isValid = true;

            foreach(char sku in skus)
            {
                isValid = products.Select(x => x.Sku).ToList().Contains(sku);
                if (!isValid)
                {
                    return isValid;
                }
            }

            return isValid;
        }
    }
}
