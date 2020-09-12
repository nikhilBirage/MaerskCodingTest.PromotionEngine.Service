using MaerskCodingTest.PromotionEngine.Service.Models;
using MaerskCodingTest.PromotionEngine.Service.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaerskCodingTest.PromotionEngine.Service.UnitTests
{
    [TestClass]
    public class PromotionsRepositoryUnitTests
    {
        private PromotionsRepository _promotionsRepository; 

        [TestInitialize]
        public void Initialize()
        {
            _promotionsRepository = new PromotionsRepository();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void AddPromotion_OnValidInput_ShouldAddPromotion()
        {
            // Arrange
            var mockPromo = new Promotion()
            {
                Id = Guid.NewGuid(),
                DiscountPerUnit = 0,
                FixedPrice = 0,
                Name = "test",
                NumberOfSKUItems = 1,
                PrmotionTypeId = PromotionsDb.promotionTypes[0].Id
            };

            // Act
            var result = _promotionsRepository.AddPromotion(mockPromo);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void AddPromotion_OnNullInput_ShouldNotAddPromotion()
        {
            // Act
            var result = _promotionsRepository.AddPromotion(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
