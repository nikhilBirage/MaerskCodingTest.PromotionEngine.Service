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
                DiscountPerUnit = 0,
                SKUs = new List<string>() { "A" }
            },
            new Promotion()
            {
                Id = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E6"),
                Name = "C & D for 130",
                PrmotionTypeId = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E4"),
                FixedPrice = 30,
                NumberOfSKUItems = 0,
                DiscountPerUnit = 0,
                SKUs = new List<string>() { "C", "D" }
            }
        };

        private List<PromotionType> mockPromotionTypes = new List<PromotionType>()
        {
            new PromotionType()
            {
                Id = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E4"),
                Name = "FixedPriceForMoreThanOneSKUs"
            },
            new PromotionType()
            {
                Id = Guid.Parse("1AA7E44F-6C5D-46CB-84D0-87720CDB58E2"),
                Name = "FixedPricePerNSKUItmes"
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

        [TestMethod]
        [TestCategory("Unit")]
        public void Post_OnValidInput_ShouldAddPromotion()
        {
            // Arrange
            Promotion promotion = new Promotion();
            mockRepository.Setup(x => x.AddPromotion(It.IsAny<Promotion>())).Returns(true);

            // Act
            var result = _promotionsController.Post(promotion);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Post_OnNullInput_ShouldNotAddPromotion()
        {
            // Arrange
            mockRepository.Setup(x => x.AddPromotion(It.IsAny<Promotion>())).Returns(true);

            // Act
            var result = _promotionsController.Post(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_OnValdInput_ShouldCalculateBasedOnPromotion_Scenario1()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
               PromotionId = "1AA7E44F-6C5D-46CB-84D0-87720CDB58E3",
               Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 3
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[0]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns(mockPromotionTypes[1]);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(mockRequest);

            // Assert
            Assert.AreEqual(130, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_OnValdInput_ShouldCalculateBasedOnPromotion_Scenario2()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
                PromotionId = "1AA7E44F-6C5D-46CB-84D0-87720CDB58E3",
                Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 5
                   },
                   new SkuRequest()
                   {
                       Name = "B",
                       Rate = 30,
                       SkuQuantity = 2
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[0]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns(mockPromotionTypes[1]);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(mockRequest);

            // Assert
            Assert.AreEqual(290, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_WhenPromotionNotFound_ShouldReturnZero()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
                PromotionId = "1AA7E44F-6C5D-46CB-84D0-87720CDB58E3",
                Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 1
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns((Promotion)null);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns(mockPromotionTypes[1]);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(mockRequest);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_WhenPromotionTypeNotFound_ShouldReturnZero()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
                PromotionId = "1AA7E44F-6C5D-46CB-84D0-87720CDB58E3",
                Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 1
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[0]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns((PromotionType)null);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(mockRequest);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_WhenRequestIsNull_ShouldReturnZero()
        {
            // Arrange
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[0]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns((PromotionType)null);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_WhenPromotionIdIsNull_ShouldReturnZero()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
                PromotionId = null,
                Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 1
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[0]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns((PromotionType)null);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_OnValdInput_ShouldCalculateBasedOnPromotion_Promo2_Scenario1()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
                PromotionId = "1AA7E44F-6C5D-46CB-84D0-87720CDB58E3",
                Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 2
                   },
                   new SkuRequest()
                   {
                       Name = "C",
                       Rate = 50,
                       SkuQuantity = 1
                   },
                    new SkuRequest()
                   {
                       Name = "D",
                       Rate = 50,
                       SkuQuantity = 1
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[1]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns(mockPromotionTypes[0]);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(mockRequest);

            // Assert
            Assert.AreEqual(130, result);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void CalculateOrderWithPromotion_OnValdInput_ShouldCalculateBasedOnPromotion_Promo2_Scenario2()
        {
            // Arrange
            var mockRequest = new CalculatePromotionRequest()
            {
                PromotionId = "1AA7E44F-6C5D-46CB-84D0-87720CDB58E3",
                Skus = new List<SkuRequest>()
               {
                   new SkuRequest()
                   {
                       Name = "A",
                       Rate = 50,
                       SkuQuantity = 2
                   },
                   new SkuRequest()
                   {
                       Name = "C",
                       Rate = 50,
                       SkuQuantity = 2
                   }
               }
            };
            mockRepository.Setup((x) => x.GetPromotion(It.IsAny<Guid>())).Returns(mockPromotions[1]);
            mockRepository.Setup((x) => x.GetPromotionType(It.IsAny<Guid>())).Returns(mockPromotionTypes[0]);

            // Act
            var result = _promotionsController.CalculateOrderWithPromotion(mockRequest);

            // Assert
            Assert.AreEqual(200, result);
        }
    }
}
