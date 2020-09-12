using MaerskCodingTest.PromotionEngine.Service.Models;
using MaerskCodingTest.PromotionEngine.Service.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    public class PromotionsController : ControllerBase
    { 
        private IPromotionsRepository _promotionsRepository;

        public PromotionsController(IPromotionsRepository promotionsRepository)
        {
            _promotionsRepository = promotionsRepository;
        }

        [HttpGet]
        public IEnumerable<Promotion> Get()
        {
            return _promotionsRepository.GetPromotions();
        }

        [HttpGet]
        [Route("promotion-types")]
        public IEnumerable<PromotionType> GetPromotionTypes() 
        {
            return _promotionsRepository.GetPromotionTypes();
        }

        [HttpGet("{id}")]
        public Promotion Get(Guid id)
        {
            return _promotionsRepository.GetPromotion(id);
        }

        [HttpPost]
        public bool Post([FromBody] Promotion promotion)
        {
            if (promotion == null) return false;

            return _promotionsRepository.AddPromotion(promotion);
        }

        [HttpPost]
        [Route("calculate-order-with-promotion")]
        public double CalculateOrderWithPromotion([FromBody] CalculatePromotionRequest promotionRequest)
        {
            if (promotionRequest == null || promotionRequest.PromotionId == null) return 0;

            return PromotionEngine(promotionRequest);
        }

        private double PromotionEngine(CalculatePromotionRequest promotionRequest)
        {
            var promotion = _promotionsRepository.GetPromotion(Guid.Parse(promotionRequest.PromotionId));
            if (promotion == null)
            {
                return 0;
            }

            var promoType = _promotionsRepository.GetPromotionType(promotion.PrmotionTypeId);
            if (promoType == null)
            {
                return 0;
            }

            switch (promoType.Name)
            {
                case "FixedPricePerNSKUItmes":
                    double totalCalculatedPromoAmount = 0;
                    
                    promotionRequest.Skus.ForEach((checkoutSku) =>
                    {
                        bool isIncludedInPromo = promotion.SKUs.Contains(checkoutSku.Name);
                        if (isIncludedInPromo && checkoutSku.SkuQuantity >= promotion.NumberOfSKUItems)
                        {
                            double remainingQuantityCal = (checkoutSku.SkuQuantity % promotion.NumberOfSKUItems) * checkoutSku.Rate;
                            double fixedQuantityCalculation = checkoutSku.SkuQuantity / promotion.NumberOfSKUItems * promotion.FixedPrice;
                            totalCalculatedPromoAmount += fixedQuantityCalculation + remainingQuantityCal;
                        }
                        else
                        {
                            totalCalculatedPromoAmount += checkoutSku.SkuQuantity * checkoutSku.Rate;
                        }
                    });
                    
                    return totalCalculatedPromoAmount;

                case "FixedPriceForMoreThanOneSKUs":
                    double totalCalculatedPromoAmountCase2 = 0;
                    var matchedList = new List<SkuRequest>();
                    promotionRequest.Skus.ForEach((checkoutSku) =>
                    {
                        bool isIncludedInPromo = promotion.SKUs.Contains(checkoutSku.Name);
                        if (isIncludedInPromo)
                        {
                            matchedList.Add(checkoutSku);
                        }
                        else
                        {
                            totalCalculatedPromoAmountCase2 += checkoutSku.SkuQuantity * checkoutSku.Rate;
                        }
                    });
                    if(matchedList.Count == promotion.SKUs.Count)
                    {
                        totalCalculatedPromoAmountCase2 += promotion.FixedPrice;
                    }
                    else
                    {
                        matchedList.ForEach((sku) =>
                        {
                            totalCalculatedPromoAmountCase2 += sku.SkuQuantity * sku.Rate;
                        });
                    }
                    return totalCalculatedPromoAmountCase2;
            };
            return 0;
        }
    }
}
