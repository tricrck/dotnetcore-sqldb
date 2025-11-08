using DotNetCoreSqlDb.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Enable Azure App Service logging
builder.Logging.AddAzureWebAppDiagnostics();

// Add services to the container
builder.Services.AddControllersWithViews();

// ✅ Use SQL Server with connection string from App Service
builder.Services.AddDbContext<MyDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ✅ Automatically apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyDatabaseContext>();
    db.Database.Migrate(); // Creates database/tables if they don't exist
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // Optional
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
