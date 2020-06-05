using System.Collections.Generic;

namespace SimplePromotionEngine.Contract
{
    public interface IPromotionEngine
    {
        double ApplyPromotion(List<char> skus);
    }
}
