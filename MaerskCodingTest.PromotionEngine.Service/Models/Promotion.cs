using System;
using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.Models
{
    public class Promotion
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<string> SKUs { get; set; }

        public int NumberOfSKUItems { get; set; }

        public int FixedPrice { get; set; }

        public int DiscountPerUnitPrice { get; set; }

        public Guid PrmotionTypeId { get; set; }
    }
}
