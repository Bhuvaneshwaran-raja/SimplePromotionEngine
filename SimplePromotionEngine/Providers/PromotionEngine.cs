using SimplePromotionEngine.Contract;
using SimplePromotionEngine.Extensions;
using SimplePromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplePromotionEngine.Providers
{
    public class PromotionEngine : IPromotionEngine
    {
        // This should either follow DI or any other design pattern shouldn't be used directly.
        // To keep it simple for demo directly initialized
        private readonly IProduct _productProvider = new ProductProvider();

        public double ApplyPromotion(List<char> skus)
        {
            try
            {
                //Foremost validation to check the product is valid
                if (skus.IsValidProduct(_productProvider.GetProducts()))
                {
                    return PromotionProcessor(skus);
                }
                else
                {
                    throw new Exception("Cart contains invalid product(s)");
                }
            }
            catch (Exception ex) // For this demo generic handling of exception. Advised to handled by specific type
            {
                throw ex;
            }
        }

        private double PromotionProcessor(List<char> skus)
        {
            double totalCartValue = 0;

            var distinctProducts = skus.Distinct().ToList();

            if (distinctProducts.Contains('C') & distinctProducts.Contains('D'))
            {
                decimal combinedProductTotal = 0;

                Product productCInfo = _productProvider.GetProduct('C');
                Product productDInfo = _productProvider.GetProduct('D');

                var countProductC = skus.Count(x => x == 'C');
                var countProductD = skus.Count(x => x == 'D');

                if (countProductC == countProductD)
                {
                    combinedProductTotal = (countProductC + countProductD) / 2 * 30;
                }
                else if (countProductC > countProductD)
                {
                    var productCDifference = countProductC - countProductD;
                    var comboPrice = ((countProductC + countProductD) - productCDifference) * 30;
                    combinedProductTotal = comboPrice + (productCDifference * (int)productCInfo.Price);
                }
                else if (countProductD > countProductC)
                {
                    var productDDifference = countProductD - countProductC;
                    var comboPrice = ((countProductC + countProductD) - productDDifference) * 30;
                    combinedProductTotal = comboPrice + (productDDifference * (int)productDInfo.Price);
                }

                distinctProducts.Remove('C');
                distinctProducts.Remove('D');

                totalCartValue = Convert.ToDouble(combinedProductTotal);
            }

            foreach (char prod in distinctProducts)
            {
                Product product = _productProvider.GetProduct(prod);
                PromotionRule promoRule = _productProvider.GetPromotion(prod);
                totalCartValue += Convert.ToDouble(PromotionCalculation(product, skus.Count(x => x == prod), promoRule));
            }

            return totalCartValue;
        }

        private decimal PromotionCalculation(Product product, int count, PromotionRule promoRule)
        {
            int productTotal = 0;

            if (promoRule.Value > 0)
            {
                decimal validPromoItems = Convert.ToDecimal(count) / promoRule.PromoItems;

                if ((validPromoItems % 1) == 0)
                {
                    productTotal += (int)validPromoItems * promoRule.Value;
                }
                else
                {
                    int eligiblePromoApplies = (int)Math.Truncate(validPromoItems);
                    var promoAppliedProducts = eligiblePromoApplies * promoRule.PromoItems;
                    var remainingProdcts = count - promoAppliedProducts;
                    productTotal = (eligiblePromoApplies * promoRule.Value) + (remainingProdcts * (int)product.Price);

                }
            }
            else
            {
                productTotal = count * (int)product.Price;
            }

            return productTotal;
        }
    }
}
