using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SimplePromotionEngineTests
{
    [TestClass]
    public class PromotionTests
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))] Ideal way of writing unit test for exception cases
        // Here we are going to have try catch block for better understanding
        public void PromotionEngine_Is_Valid_Product()
        {
            try
            {
                SimplePromotionEngine.Providers.PromotionEngine promotionEngine = new SimplePromotionEngine.Providers.PromotionEngine();
                var invalidProducts = new List<char> { 'Z', 'A' };
                promotionEngine.ApplyPromotion(invalidProducts);
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Cart contains invalid product(s)", ex.Message);
            }
        }

        [TestMethod]
        public void PromotionEngine_With_Scenario_A()
        {
            SimplePromotionEngine.Providers.PromotionEngine promotionEngine = new SimplePromotionEngine.Providers.PromotionEngine();
            var invalidProducts = new List<char> { 'A', 'B', 'C' };
            var total = promotionEngine.ApplyPromotion(invalidProducts);
            Assert.IsTrue(total == 100);
        }

        [TestMethod]
        public void PromotionEngine_With_Scenario_B()
        {
            SimplePromotionEngine.Providers.PromotionEngine promotionEngine = new SimplePromotionEngine.Providers.PromotionEngine();
            var invalidProducts = new List<char> { 'A', 'A', 'A', 'A', 'A', 'B', 'B', 'B', 'B', 'B', 'C' };
            var total = promotionEngine.ApplyPromotion(invalidProducts);
            Assert.IsTrue(total == 370);
        }

        [TestMethod]
        public void PromotionEngine_With_Scenario_C()
        {
            SimplePromotionEngine.Providers.PromotionEngine promotionEngine = new SimplePromotionEngine.Providers.PromotionEngine();
            var invalidProducts = new List<char> { 'A', 'A', 'A', 'B', 'B', 'B', 'B', 'B', 'C', 'D' };
            var total = promotionEngine.ApplyPromotion(invalidProducts);
            Assert.IsTrue(total == 280);
        }

    }
}
