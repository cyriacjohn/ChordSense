namespace ChordSense.Api
{
    using Microsoft.EntityFrameworkCore;
    using ChordSense.Api.Data;
    using ChordSense.Api.Services;
    using ChordSense.Api.Services.Interfaces;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database
            builder.Services.AddDbContext<ChordSenseDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Controllers
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddOpenApi();

            // HttpClient 
            //builder.Services.AddHttpClient();

            builder.Services.AddHttpClient<IAIAnalysisService, AIAnalysisService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(10);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            app.UseCors("AllowAngular");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}