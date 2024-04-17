using LoyaltyPointsExchangeSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddDbContextPool<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBConnection")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric =false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});
var app = builder.Build();

var env = builder.Environment;

if (env.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseStaticFiles();


app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoint =>
{
    endpoint.MapDefaultControllerRoute();
});

//app.MapGet("/", () => "Hello World!");

app.Run();
