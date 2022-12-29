using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Bok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bok.Data.Static;
using Microsoft.AspNetCore.Identity;
using System.Drawing;

namespace Bok.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Publishers.Any())
                {
                    context.Publishers.AddRange(new List<Publisher>()
                    {
                        new Publisher()
                        {
                            Name = "Kim Đồng",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first publisher"
                        },
                        new Publisher()
                        {
                            Name = "NBX trẻ",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            Description = "This is the description of the first publisher"
                        },
                        new Publisher()
                        {
                            Name = "NXB Thanh thiếu niên",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                            Description = "This is the description of the first publisher"
                        },
                       
                    });
                    context.SaveChanges();
                }
                //Actors
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            FullName = "Will Smith",
                            Bio = "This is the Bio of the first author",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg"

                        },
                        new Author()
                        {
                            FullName = "Matt LeBlanc",

                            Bio = "This is the Bio of the second authorr",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Author()
                        {
                            FullName = "Angelina Jolie",
                            Bio = "This is the Bio of the second authorr",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                       
                    });
                    context.SaveChanges();
                }

                //Movies
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",

                           PublisherId = 1,
                            BookCategory = BookCategory.Documentary
                        },
                        new Book()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",

                            PublisherId = 2,
                            BookCategory = BookCategory.Action
                        },
                       
                        new Book()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",

                            PublisherId = 3,
                            BookCategory = BookCategory.Documentary
                        },
                       
                    });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Authors_Books.Any())
                {
                    context.Authors_Books.AddRange(new List<Author_Book>()
                    {
                        new Author_Book()
                        {
                            AuthorId = 1,
                            BookId = 1
                        },
                       

                         new Author_Book()
                        {
                            AuthorId = 1,
                            BookId = 2
                        },
                        
                        new Author_Book()
                        {
                            AuthorId = 1,
                            BookId = 3
                        },


                    });
                    context.SaveChanges();
                }
            }

      
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Admin@1");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}

   

       
   