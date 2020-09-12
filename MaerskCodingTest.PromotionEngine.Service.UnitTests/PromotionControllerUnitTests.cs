using MaerskCodingTest.PromotionEngine.Service.Controllers;
using MaerskCodingTest.PromotionEngine.Service.Models;
using MaerskCodingTest.PromotionEngine.Service.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace MaerskCodingTest.PromotionEngine.Service.UnitTests
{
    [TestClass]
    public class PromotionControllerUnitTests
    {
        private PromotionsController _promotionsController;
        private Mock<IPromotionsRepository> mockRepository;
        private List<Promotion> mockPromotions = new List<Promotion>()
        {
            new Promotion()
            {
                Id = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E3"),
                Name = "3 of A's for 130",
                PrmotionTypeId = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E2"),
                FixedPrice = 130,
                NumberOfSKUItems = 3,
                DiscountPerUnitPrice = 0,
                SKUs = new List<string>() { "A" }
            }
        };

        [TestInitialize]
        public void Initialize()
        {
            mockRepository = new Mock<IPromotionsRepository>();

            _promotionsController = new PromotionsController(mockRepository.Object);

            mockRepository.Setup((x) => x.GetPromotions()).Returns(mockPromotions);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Get_Get_ReturnPromotions()
        {
            // Act
            var result = _promotionsController.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetById_OnValidId_ReturnPromotion()
        {
            // Arrange
            var id = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E3");
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[0]);

            // Act
            var result = _promotionsController.Get(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetById_OnInValidId_ReturnNullPromotion()
        {
            // Arrange
            var id = Guid.NewGuid();
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns((Promotion)null);

            // Act
            var result = _promotionsController.Get(id);

            // Assert
            Assert.IsNull(result);
        }

    }
}
