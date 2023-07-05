using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQWatermark.WEB.BackgroundServices;
using RabbitMQWatermark.WEB.Models;
using RabbitMQWatermark.WEB.Services;

namespace RabbitMQWatermark.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "productDb");
            });
            builder.Services.AddSingleton(sp=> new ConnectionFactory() { Uri=new Uri(builder.Configuration.GetConnectionString("RabbitMQ"))});

            builder.Services.AddSingleton<RabbitMQClientService>();
            builder.Services.AddSingleton<RabbitMQPublisher>();

            builder.Services.AddHostedService<ImageWatermarkProcessBackgroundService>();

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
                pattern: "{controller=Products}/{action=Create}/{id?}");

            app.Run();
        }
    }
}