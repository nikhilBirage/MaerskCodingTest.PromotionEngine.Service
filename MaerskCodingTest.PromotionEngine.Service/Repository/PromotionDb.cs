using MaerskCodingTest.PromotionEngine.Service.Models;
using System;
using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.Repository
{
    public static class PromotionsDb
    {
        public static List<PromotionType> promotionTypes = new List<PromotionType>()
        {
            new PromotionType()
            {
                Id = Guid.Parse("c2ffb656-9d18-40ab-8074-97596f8ecadc"),
                Name = "FixedPricePerNSKUItmes"
            },
            new PromotionType()
            {
                Id = Guid.Parse("10a9ad42-6884-4cf6-9529-babaed77ba4e"),
                Name = "FixedPriceForMoreThanOneSKUs"
            }
        };

        public static List<Promotion> promotions = new List<Promotion>()
        {
            new Promotion()
            {
                Id = Guid.Parse("b2522dca-2522-46e4-ba83-70eb47effcfd"),
                Name = "3 of A's for 130",
                PrmotionTypeId = promotionTypes[0].Id,
                FixedPrice = 130,
                NumberOfSKUItems = 3,
                DiscountPerUnit = 0,
                SKUs = new List<string>() { "A" }
            },
             new Promotion()
            {
                Id = Guid.Parse("6286c124-30fd-46c8-8c9e-ac7d58ea38f6"),
                Name = "2 of B's for 45",
                PrmotionTypeId = promotionTypes[0].Id,
                FixedPrice = 45,
                NumberOfSKUItems = 2,
                DiscountPerUnit = 0,
                SKUs = new List<string>() { "B" }
            },
             new Promotion()
            {
                Id = Guid.Parse("d1e0c161-12db-4d3a-b00e-65a2c4ec8b07"),
                Name = "C & D for 30",
                PrmotionTypeId = promotionTypes[1].Id,
                FixedPrice = 30,
                NumberOfSKUItems = 0,
                DiscountPerUnit = 0,
                SKUs = new List<string>() { "C" , "D" }
            }
        };
    }
}
