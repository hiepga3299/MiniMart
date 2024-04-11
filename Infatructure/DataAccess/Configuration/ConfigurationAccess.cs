using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniMart.Application.Services;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.Dapper;
using MiniMart.Infatructure.Repository;

namespace MiniMart.Infatructure.DataAccess.Configuration
{
	public static class ConfigurationAccess
	{
		public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<MiniMartDbContext>(options =>
						options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>()
						.AddEntityFrameworkStores<MiniMartDbContext>();
			services.ConfigureApplicationCookie(option =>
			{
				option.Cookie.Name = "MiniMartCookie";
				option.ExpireTimeSpan = TimeSpan.FromHours(1);
				option.LoginPath = "/admin/authentication/login";
				option.SlidingExpiration = true;
			});
			services.Configure<IdentityOptions>(option =>
			{
				option.Lockout.AllowedForNewUsers = true;
				option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
				option.Lockout.MaxFailedAccessAttempts = 3;
			});
		}

		public static void RegisterDI(this IServiceCollection services)
		{
			services.AddTransient<ISQLQueryHandler, SQLQueryHandler>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<IImageService, ImageService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IRolesService, RolesService>();
			//services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<ICartService, CartService>();
			services.AddTransient<IOrderService, OrderService>();
			services.AddTransient<IUserAddressService, UserAddressService>();
		}

		public static void AutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}
	}
}
