
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Talabat.Repository.Data;

namespace Talabat
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {

           

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });




            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Has Been Occured During Apply The Migration");
            }


          




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
