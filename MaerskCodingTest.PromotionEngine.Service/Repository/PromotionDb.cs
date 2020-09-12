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
                Id = Guid.NewGuid(),
                Name = "FixedPricePerNSKUItmes"
            },
            new PromotionType()
            {
                Id = Guid.NewGuid(),
                Name = "FixedPriceForMoreThanOneSKUs"
            }
        };

        public static List<Promotion> promotions = new List<Promotion>()
        {
            new Promotion()
            {
                Id = Guid.NewGuid(),
                Name = "3 of A's for 130",
                PrmotionTypeId = promotionTypes[0].Id,
                FixedPrice = 130,
                NumberOfSKUItems = 3,
                DiscountPerUnitPrice = 0,
                SKUs = new List<string>() { "A" }
            },
             new Promotion()
            {
                Id = Guid.NewGuid(),
                Name = "2 of B's for 45",
                PrmotionTypeId = promotionTypes[0].Id,
                FixedPrice = 45,
                NumberOfSKUItems = 2,
                DiscountPerUnitPrice = 0,
                SKUs = new List<string>() { "B" }
            },
             new Promotion()
            {
                Id = Guid.NewGuid(),
                Name = "C & D for 30",
                PrmotionTypeId = promotionTypes[1].Id,
                FixedPrice = 30,
                NumberOfSKUItems = 0,
                DiscountPerUnitPrice = 0,
                SKUs = new List<string>() { "C" , "D" }
            }
        };

    }
}
