using BlogProject.App.Mappers;
using BlogProject.App.Utilities.ILogging;
using BlogProject.Core.DomainModels.Models;
using BlogProject.Infrastructure.Contexts;
using BlogProject.Infrastructure.Utilitites;
using BlogProject.Infrastructure.Utilitites.Logging;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(Mapping));
            builder.Services.AddScoped<ILoggerService, LoggerService>();
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddServiceInfrastructure();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}