using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PcStoreBackend.Data;
using PcStoreBackend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure the database context using SQL Server.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with roles for Admin and User
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Password policy settings can be modified here
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>() // Adding roles (Admin, User)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

// Optionally, add CORS if needed (uncomment if needed)
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAll",
//         policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// });

var app = builder.Build();

// Seed admin user and roles when the application starts.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        await SeedData.SeedRolesAndAdminUser(roleManager, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adds the authentication middleware
app.UseAuthorization();  // Adds the authorization middleware

// Optionally, use CORS if needed (uncomment if enabled above)
// app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
