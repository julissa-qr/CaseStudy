using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        //lo que usaremos para interactuar con la base de datos
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly StoreContext _context;

        public AccountController(UserManager<User> userManager, TokenService tokenService, StoreContext context)
        {
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager; //nos ermite iniciar sesion y registrar usuarios
        }

        //LOGIN
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) //tomar parametros de dto
        {
            //primero checar si tenemos el usuario en db o sera null porue no existe en la db
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            //ver si la contra coincide dentro de la db
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            //obtener las ordenes de los usuarios
            var userBasket = await RetrieveBasket(loginDto.Username);
            var anonBasket = await RetrieveBasket(Request.Cookies["customerId"]);

            /* si ya hay un a orden en el servidor y ellos tienen
            una orden anonima, se elimina la orden de usuario y cambia el nombre del customerId
            y de la orden anonima al username*/
            if (anonBasket != null)
            {
                if (userBasket != null) _context.Baskets.Remove(userBasket);
                anonBasket.CostumerId = user.UserName; //se transfiere la orden anonima al usuario
                Response.Cookies.Delete("customerId");
                await _context.SaveChangesAsync();
            }
            //regresa el usuario porque han iniciado sesion satisfactoriamente
            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                //si tenemos la orden y no tenemos ordenes anonimas, regresa la orden de usuario
                Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            //crear usuario
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };

            //checar resultados (username puede duplicarse)
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            // si tenemos username duplicado, email no es valido o si pass no cumple con los requisitos
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            //se registra el usuario y lo agregamos como Member
            await _userManager.AddToRoleAsync(user, "Member");
            return StatusCode(201);
        }

        [Authorize] //indicar que se tiene que autorizar (token)
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //ver si tenemos usuario
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userBasket = await RetrieveBasket(User.Identity.Name);

            // regresa el email, carrito y token
            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Basket = userBasket?.MapBasketToDto()
            };
        }

        //nos da la direccion del usuario
        [Authorize] 
        [HttpGet("savedAddress")]
        public async Task<ActionResult<UserAddress>> GetSavedAddress()
        {
            return await _userManager.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(user => user.Address)
                .FirstOrDefaultAsync();
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

    }
}