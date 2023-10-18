using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IFlightService,FlightService>();//instance a chaque appele
builder.Services.AddTransient<IPlaneService,PlaneService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//instance a chaque requette
builder.Services.AddTransient(typeof(IService<>) , typeof(Service<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Flight}/{action=Index}/{id?}");

app.Run();
