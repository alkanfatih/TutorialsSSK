using _1_Pagination.ActionFilters;
using _1_Pagination.AutoMappers;
using _1_Pagination.Contexts;
using _1_Pagination.Loggers;
using _1_Pagination.Models;
using _1_Pagination.TokenServices;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;

namespace _1_Pagination
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Logger Service
            builder.Services.AddSingleton<ILoggerService, LoggerService>();

            //AppDbContext
            var conn = builder.Configuration.GetConnectionString("DefaultConn");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

            //IdentityDbContext
            builder.Services.AddDbContext<AppIdDbContext>(options => options.UseSqlServer(conn));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options => 
            { 
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppIdDbContext>();

            //JWT Token Service
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];
            builder.Services.AddAuthentication(options =>
            {
                //Authentication schemesý için bearer kullanýyoruz.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters 
            { 
                ValidateIssuer = true, //Üreteni doðrula
                ValidateAudience = true, //Alýcýyý doðrula
                ValidateLifetime = true, //süre
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                //Anahtar Tanýmlama
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            });

            //MyTokenService
            builder.Services.AddScoped<MyTokenService>();

            builder.Services.AddAutoMapper(typeof(Mapping));

            builder.Services.AddScoped<ValidationFilterAttribute>();

            //HýzSýnýrlandýmasý
            builder.Services.AddMemoryCache();
            builder.Services.AddHttpContextAccessor();

            builder.Services.Configure<IpRateLimitOptions>(opt => 
            {
                opt.GeneralRules = new List<RateLimitRule>()
                {
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 3,
                        Period = "1m"
                    }
                };
            });

            builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //sýnýrlama...
            app.UseIpRateLimiting();

            //Login
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}