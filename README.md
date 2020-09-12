# MaerskCodingTest.PromotionEngine.Service
Promotion Service - Calculate Order value when promotions applied to total order.
Note: This service is using static data for promotions and promotion types as follows: 

promotion types:
[
  {
    "id": "c2ffb656-9d18-40ab-8074-97596f8ecadc",
    "name": "FixedPricePerNSKUItmes"
  },
  {
    "id": "10a9ad42-6884-4cf6-9529-babaed77ba4e",
    "name": "FixedPriceForMoreThanOneSKUs"
  }
]

promotions:
[
  {
    "id": "b2522dca-2522-46e4-ba83-70eb47effcfd",
    "name": "3 of A's for 130",
    "skUs": [
      "A"
    ],
    "numberOfSKUItems": 3,
    "fixedPrice": 130,
    "discountPerUnit": 0,
    "prmotionTypeId": "c2ffb656-9d18-40ab-8074-97596f8ecadc"
  },
  {
    "id": "6286c124-30fd-46c8-8c9e-ac7d58ea38f6",
    "name": "2 of B's for 45",
    "skUs": [
      "B"
    ],
    "numberOfSKUItems": 2,
    "fixedPrice": 45,
    "discountPerUnit": 0,
    "prmotionTypeId": "c2ffb656-9d18-40ab-8074-97596f8ecadc"
  },
  {
    "id": "d1e0c161-12db-4d3a-b00e-65a2c4ec8b07",
    "name": "C & D for 30",
    "skUs": [
      "C",
      "D"
    ],
    "numberOfSKUItems": 0,
    "fixedPrice": 30,
    "discountPerUnit": 0,
    "prmotionTypeId": "10a9ad42-6884-4cf6-9529-babaed77ba4e"
  }
]


use the swagger to test the calculate-order-with-promotion method with calculate promotion request as follows:
(order should have only one promotion applied as per order)

scenario with promo 1 and promo type 1: 
{
  "skus": [
    {
      "skuQuantity": 5,
      "name": "A",
      "rate": 50
    },
    {
      "skuQuantity": 1,
      "name": "B",
      "rate": 50
    }
  ],
  "promotionId": "b2522dca-2522-46e4-ba83-70eb47effcfd"
}
Result:  280


scenario with promo 2 and promo type 1: 
{
  "skus": [
    {
      "skuQuantity": 2,
      "name": "A",
      "rate": 50
    },
    {
      "skuQuantity": 2,
      "name": "B",
      "rate": 50
    }
  ],
  "promotionId": "6286c124-30fd-46c8-8c9e-ac7d58ea38f6"
}
Result:  145

scenario with promo 3 and promo type 2: 
{
  "skus": [
    {
      "skuQuantity": 1,
      "name": "A",
      "rate": 50
    },
    {
      "skuQuantity": 1,
      "name": "B",
      "rate": 50
    },
    {
      "skuQuantity": 1,
      "name": "C",
      "rate": 50
    },
    {
      "skuQuantity": 1,
      "name": "D",
      "rate": 50
    }
  ],
  "promotionId": "d1e0c161-12db-4d3a-b00e-65a2c4ec8b07"
}
Result:  130

