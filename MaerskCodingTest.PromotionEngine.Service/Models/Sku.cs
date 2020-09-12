namespace MaerskCodingTest.PromotionEngine.Service.Models
{
    public class Sku
    {
        public string Name { get; set; }
        public int Rate { get; set; }
    }

    public class SkuRequest : Sku
    {
        public int SkuQuantity { get; set; }
    }

}
