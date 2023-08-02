using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Application.Services;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using AmazonAdmin.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace AmazonAdminDashboardMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ApplicationContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
			}).AddEntityFrameworkStores<ApplicationContext>();
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

			builder.Services.AddScoped<IProductReposatory, ProductReposatory>();
			builder.Services.AddScoped<IProductServices, ProductSrvices>();
			builder.Services.AddScoped<ICategoryReposatory, CategoryReposatory>();
			builder.Services.AddScoped<IcategoryServices, CategoryService>();
			builder.Services.AddScoped<ISubCategoryReposatory, SubCategoryReposatory>();
			builder.Services.AddScoped<ISubcategoryServices, SubCategoryService>();
            builder.Services.AddScoped<IOrderReposatory, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IOrderItemService, OrderItemServices>();
            builder.Services.AddScoped<IOrderItemReposatory, OrderItemReposatory>();
			builder.Services.AddScoped<IImageReposatory, ImageReposatory>();
			builder.Services.AddScoped<IUserReposatory, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<>
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<ICityReposatory, CityReposatory>();
            builder.Services.AddScoped<IcountryReposatory, CountryReposatory>();
            builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressReposatory>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<ICountryServices, CountryServices>();
			builder.Services.AddDirectoryBrowser();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
			var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "uploadedImages"));
			var requestPath = "/uploadedImages";
			// Enable displaying browser links.
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = fileProvider,
				RequestPath = requestPath
			});

			app.UseDirectoryBrowser(new DirectoryBrowserOptions
			{
				FileProvider = fileProvider,
				RequestPath = requestPath
			});
			app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                

            app.Run();
        }
    }
}