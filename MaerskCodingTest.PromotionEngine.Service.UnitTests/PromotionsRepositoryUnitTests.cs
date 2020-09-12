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

        [TestMethod]
        [TestCategory("Unit")]
        public void GetPromotions_GetPromotions_ReturnPromotions()
        {
            // Act
            var result = _promotionsRepository.GetPromotions();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetPromotionTypes_GetPromotionTypes_ReturnPromotionTypes()
        {
            // Act
            var result = _promotionsRepository.GetPromotionTypes();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetPromotionById_OnValidId_ReturnPromotion()
        {
            // Arrange
            var id = PromotionsDb.promotions[0].Id;

            // Act
            var result = _promotionsRepository.GetPromotion(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetPromotionTypesById_OnValidId_ReturnPromotionTypes()
        {
            // Arrange
            var id = PromotionsDb.promotionTypes[0].Id;

            // Act
            var result = _promotionsRepository.GetPromotionType(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }
    }
}
