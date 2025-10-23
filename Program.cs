
using lista10.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => { 
	options.IdleTimeout = TimeSpan.FromDays(7);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<ShopDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

/*

app.MapControllerRoute(
                   name: "game_draw",
                   pattern: "{controller=Game}/{action=Draw}"
                  );

app.MapControllerRoute(
   name: "game_set",
   pattern: "{controller=Game}/{action=Set}/{set_value}"
   );

app.MapControllerRoute(
   name: "game_draw",
   pattern: "Game/Guess/{guess_value1},{guess_value2}",
   defaults: new { controller = "Game", action = "Guess" });

app.MapControllerRoute(
   name: "game_index",
   pattern: "{controller=Game}/{action=Index}");
*/

app.Run();
