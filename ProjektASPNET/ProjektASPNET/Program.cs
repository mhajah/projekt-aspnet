using Microsoft.EntityFrameworkCore;
using ProjektASPNET.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


var app = builder.Build();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
