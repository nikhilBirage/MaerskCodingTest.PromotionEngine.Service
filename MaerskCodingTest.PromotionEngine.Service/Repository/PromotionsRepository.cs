using MaerskCodingTest.PromotionEngine.Service.Models;
using System;
using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.Repository
{
    public interface IPromotionsRepository
    {
        IEnumerable<Promotion> GetPromotions();
        Promotion GetPromotion(Guid id);
        bool AddPromotion(Promotion promotion);
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

    }
}
