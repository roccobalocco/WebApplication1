using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MvcCoseinutiliContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcCoseinutiliContext"), 
        options => options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: System.TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));
//builder.Services.AddDbContext<MvcCoseinutiliContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcCoseinutiliContextPortatile"), 
//        options => options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: System.TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));
Console.WriteLine("Connessione al database avvenuta con successo");

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();