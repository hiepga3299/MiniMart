using MiniMart.Infatructure.Configuration;
using MiniMart.Infatructure.DataAccess.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDb(builder.Configuration);
builder.Services.RegisterDI();
builder.Services.AutoMapper();
builder.Services.AddSession(option =>
                                    option.IdleTimeout = TimeSpan.FromHours(1));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

app.AutoMigration().GetAwaiter().GetResult();
app.SeedData(builder.Configuration).GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
