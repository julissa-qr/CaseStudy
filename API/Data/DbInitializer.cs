using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(StoreContext context, UserManager<User> userManager)
        {
            /*vemos si tenemos algun usuario en la base de datos
            si no, creamos dos usuarios*/
            if (!userManager.Users.Any())
            {
                //usuario miembro
                var user = new User
                {
                    UserName = "juli",
                    Email = "juli@hotmail.com"
                };
                /*usamos await por el metodo async
                cuando creamos un usuario, no tenemos que usar save changes
                el metodo async se encarga por nosotros 
                es el trabajo de userManager*/
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");

                //se crea el usuario admin
                var admin = new User
                {
                    UserName = "joseadmin",
                    Email = "joseadmin@hotmail.com"
                };
                //no hay necesidad de guardar manualmente los cambios a la db
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
            }

            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                    new Product
                {
                    Name = "Gray VueJS T-Shirt",
                    Description =
                        "Nice T-shit, make you look great!",
                    Price = 20000,
                    PictureUrl = "/images/products/1.jpg",
                    Brand = "VueJS",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Light Gray AngularJS T-Shirt",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    PictureUrl = "/images/products/2.jpg",
                    Brand = "Angular",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Black React Pro T-Shirt",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    PictureUrl = "/images/products/3.jpg",
                    Brand = "React",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Yellow Redux Cool T-Shirt",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 30000,
                    PictureUrl = "/images/products/4.jpg",
                    Brand = "Redux",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Gray Node Super T-Shirt",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 25000,
                    PictureUrl = "/images/products/5.jpg",
                    Brand = "Node",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Sass Black Pro T-Shirt",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 12000,
                    PictureUrl = "/images/products/6.jpg",
                    Brand = "Awesome",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "HTML5 Gray Pro T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 10000,
                    PictureUrl = "/images/products/7.jpg",
                    Brand = "HTML",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Purple Github Wow T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 80000,
                    PictureUrl = "/images/products/8.jpg",
                    Brand = "Github",
                    Type = "T-Shirts",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Red Bulma T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 15000,
                    PictureUrl = "/images/products/9.jpg",
                    Brand = "CSS",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Blue & Gray Typescript T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 18000,
                    PictureUrl = "/images/products/10.jpg",
                    Brand = "TS",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Blue Drupal T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 15000,
                    PictureUrl = "/images/products/11.jpg",
                    Brand = "Drupal",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Lemon JavaScript T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 16000,
                    PictureUrl = "/images/products/12.jpg",
                    Brand = "JS",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Black & Pink GraphQL T-Shirt",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 14000,
                    PictureUrl = "/images/products/13.jpg",
                    Brand = "GraphQL",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Red WordPress T-Shirt",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 25000,
                    PictureUrl = "/images/products/14.jpg",
                    Brand = "Press",
                    Type = "T-Shirt",
                    QuantityInStock = 100
                },
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}