using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;

        public bool IsEssential { get; private set; }

        public BasketController(StoreContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            //the basket wt the items and info
            /*var basket = await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.CostumerId == Request.Cookies["customerId"]);
        */
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            return new BasketDto
            {
                Id = basket.Id,
                CustomerId = basket.CostumerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Product.QuantityInStock
                }).ToList()
            };
        }


        [HttpPost]//     api/basket?productId=2quantity=3
        public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
        {
            //get basket
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();

            //get product related to the item
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            
            //add item
            basket.AddItem(product, quantity);

            //save changes
            var result = await _context.SaveChangesAsync() > 0;
            
            if(result) return StatusCode(201);

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteBasketItem(int productId, int quantity)
        {
            //get basket
            // remove item or reduce quantity
            // save changes
            return Ok();
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await _context.Baskets
                 .Include(i => i.Items)
                 .ThenInclude(p => p.Product)
                 .FirstOrDefaultAsync(x => x.CostumerId == Request.Cookies["customerId"]);
        }

        private Basket CreateBasket()
        {
            var customerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("customerId", customerId, cookieOptions);
            var basket = new Basket { CostumerId = customerId };
            _context.Baskets.Add(basket);
            return basket;
        }

    }
}