using MaerskCodingTest.PromotionEngine.Service.Models;
using System;
using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.Repository
{
    public interface IPromotionsRepository
    {
        // Promotion
        IEnumerable<Promotion> GetPromotions();
        Promotion GetPromotion(Guid id);
        bool AddPromotion(Promotion promotion);

        // Promotion type
        IEnumerable<PromotionType> GetPromotionTypes();
        PromotionType GetPromotionType(Guid id);
    }

    public class PromotionsRepository : IPromotionsRepository
    {
        public IEnumerable<Promotion> GetPromotions()
        {
            return PromotionsDb.promotions;
        }

        public Promotion GetPromotion(Guid id)
        {
            return PromotionsDb.promotions.Find(x => x.Id == id);
        }

        public bool AddPromotion(Promotion promotion)
        {
            if (promotion == null) return false;

            try
            {
                PromotionsDb.promotions.Add(promotion);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // Promotion type
        public IEnumerable<PromotionType> GetPromotionTypes()
        {
            return PromotionsDb.promotionTypes;
        }

        public PromotionType GetPromotionType(Guid id)
        {
            return PromotionsDb.promotionTypes.Find(x => x.Id == id);
        }
    }
}
