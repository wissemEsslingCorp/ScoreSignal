using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScoreSignal.Data;
using ScoreSignal.Hubs;
using ScoreSignal.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MGContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MGContext") ?? throw new InvalidOperationException("Connection string 'MGContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
app.MapHub<MatchHub>("/MatchHub");
app.MapHub<EvenementHub>("/EvenementHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
