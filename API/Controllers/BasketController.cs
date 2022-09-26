using System;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Http;
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

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket(GetCustomerId());

            //si no hay productos en el carrito...
            if (basket == null) return NotFound();
            return basket.MapBasketToDto();
        }

        [HttpPost]//     api/basket?productId=2quantity=3
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            //obtener carrito
            var basket = await RetrieveBasket(GetCustomerId());
            if (basket == null) basket = CreateBasket();

            //obtener producto relacionado al item
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            //agregar item
            basket.AddItem(product, quantity);

            //guardar cambios
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteBasketItem(int productId, int quantity)
        {
            //get basket
            var basket = await RetrieveBasket(GetCustomerId());
            if (basket == null) return NotFound();

            // remover item o cambiar cantidad
            basket.RemoveItem(productId, quantity);

            // guardar cambios
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing item from basket" });
        }

        private async Task<Basket> RetrieveBasket(string customerId)
        {

            if (string.IsNullOrEmpty(customerId))
            {
                Response.Cookies.Delete("customerId");
                return null;
            }

            return await _context.Baskets
                 .Include(i => i.Items)
                 .ThenInclude(p => p.Product)
                 .FirstOrDefaultAsync(x => x.CostumerId == customerId);
        }

        private string GetCustomerId()
        {
            return User.Identity?.Name ?? Request.Cookies["customerId"];
        }

        //cuando se crea la orden, vamos a darle el customerId a su usuario
        private Basket CreateBasket()
        {
            var customerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(customerId))
            {
                //y si no estarian usando ordenes anonimas
                customerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("customerId", customerId, cookieOptions);
            }
            //crea el carrito y se le asigna el customer id
            var basket = new Basket { CostumerId = customerId };
            _context.Baskets.Add(basket);
            return basket;
        }
    }
}