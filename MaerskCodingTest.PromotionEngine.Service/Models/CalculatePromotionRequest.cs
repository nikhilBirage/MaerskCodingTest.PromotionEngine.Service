using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.Models
{
    public class CalculatePromotionRequest
    {
        public List<SkuRequest> Skus { get; set; }

        public string PromotionId { get; set; }
    }
}
