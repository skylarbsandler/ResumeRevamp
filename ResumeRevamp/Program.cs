using Microsoft.EntityFrameworkCore;
using ResumeRevamp.Interfaces;
using ResumeRevamp.DataAccess;
using ResumeRevamp.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IWordsApiService, WordsApiService>();

builder.Services.AddDbContext<ResumeRevampContext>(
    options =>
        options
            .UseNpgsql(
                 builder.Configuration["RESUMEREVAMP_DBCONNECTIONSTRING"]
            )
            .UseSnakeCaseNamingConvention()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
