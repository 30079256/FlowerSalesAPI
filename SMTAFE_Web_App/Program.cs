using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMTAFE_Web_App.Areas.Identity.Data;
using SMTAFE_Web_App.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SMTAFE_Web_AppContextConnection") ?? throw new InvalidOperationException("Connection string 'SMTAFE_Web_AppContextConnection' not found.");

builder.Services.AddDbContext<SMTAFE_Web_AppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SMTAFE_Web_AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SMTAFE_Web_AppContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
