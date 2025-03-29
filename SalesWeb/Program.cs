using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SalesWebContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("SalesWebContext"), 
ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebContext")),
mySqlOptions => mySqlOptions.MigrationsAssembly("SalesWeb")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
