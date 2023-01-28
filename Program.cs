using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDotNet.Data;
namespace ToDotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ToDotNetContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ToDotNetContext") ?? throw new InvalidOperationException("Connection string 'ToDotNetContext' not found.")));

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

            // Ensure the database creation.
            // using(var scope = app.Services.CreateScope()) 
            // {
            //    var services = scope.ServiceProvider;
            //    var context = services.GetRequiredService<ToDotNetContext>();
            // }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}