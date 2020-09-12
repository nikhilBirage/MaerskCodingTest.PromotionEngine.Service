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

        [HttpGet("{id}")]
        public Promotion Get(Guid id)
        {
            return _promotionsRepository.GetPromotion(id);
        }
    }
}
