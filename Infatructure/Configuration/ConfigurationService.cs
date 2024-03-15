using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniMart.Domain.Configuration;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Configuration
{
    public static class ConfigurationService
    {
        public static async Task AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MiniMartDbContext>();
                await db.Database.MigrateAsync();
            }
        }

        public static async Task SeedData(this WebApplication webApplication, IConfiguration configuration)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MiniMartDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var defaultUser = configuration.GetSection("DefaultUser").Get<DefaultUser>() ?? new DefaultUser();
                var defaultRole = configuration.GetValue<string>("DefaultRole") ?? "SuperAdmin";

                //add roles
                var roleExist = await roleManager.RoleExistsAsync(defaultRole);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(defaultRole));
                }

                //add user
                var isExists = await userManager.FindByNameAsync(defaultUser.Username);

                if (isExists is null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = defaultUser.Username,
                        Fullname = defaultUser.Fullname,
                        Email = defaultUser.Email,
                        PhoneNumber = defaultUser.Phone,
                        IsActive = true,
                        AccessFailedCount = 0
                    };

                    var identityUser = await userManager.CreateAsync(user, defaultUser.Password);

                    if (identityUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, defaultRole);
                    }
                }

                //add product
                if (db.Product.Any()) return;

                var product = new Product[]
                {
                    new Product
                    {
                        Name = "TET-2024 T01",
                        Available = 3,
                        Price = 600000,
                        CreateOn = DateTime.Parse("2024-01-22"),
                        IsActive = true
                    },
                    new Product
                    {
                        Name = "TET-2024 T02",
                        Available = 4,
                        Price = 700000,
                        CreateOn = DateTime.Parse("2024-01-21"),
                        IsActive = true
                    },
                    new Product
                    {
                        Name = "FOOD - Cơm Cháy",
                        Available = 4,
                        Price = 90000,
                        CreateOn = DateTime.Parse("2024-01-20"),
                        IsActive = true
                    }
                };
                foreach (Product p in product)
                {
                    db.Product.Add(p);
                }
                db.SaveChanges();
                var category = new Category[]
                {
                    new Category
                    {
                        Name= "Quà Tết",
                        IsActive = true
                    },
                    new Category
                    {
                        Name = "Đồ ăn vặt",
                        IsActive = true
                    }
                };
                foreach (Category c in category)
                {
                    db.Category.Add(c);
                }
                db.SaveChanges();

                var productCategory = new ProductsCategories[]
                {
                    new ProductsCategories
                    {
                        ProductId = 1,
                        CategoryId = 1
                    },
                    new ProductsCategories
                    {
                        ProductId = 2,
                        CategoryId = 1
                    },
                    new ProductsCategories
                    {
                        ProductId = 3,
                        CategoryId = 2
                    }
                };
                foreach (ProductsCategories pc in productCategory)
                {
                    db.ProductsCategories.Add(pc);
                }
                db.SaveChanges();
            }
        }
    }
}
