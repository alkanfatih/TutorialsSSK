using BlogProject.App.IRepositories;
using BlogProject.App.Services;
using BlogProject.App.Services.IServices;
using BlogProject.App.Utilities.ILogging;
using BlogProject.App.Utilities.IUnitOfWorks;
using BlogProject.Core.DomainModels.Models;
using BlogProject.Infrastructure.Contexts;
using BlogProject.Infrastructure.Repositories;
using BlogProject.Infrastructure.Utilitites.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Utilitites
{
    public static class ServiceRegistration
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration = null)
        {


            services.AddScoped<IAppUserRepo, AppUserRepo>();
            services.AddScoped<IArticleRepo, ArticleRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();

            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategorService, CategoryService>();
            services.AddScoped<IUnitOfWork, BlogProject.Infrastructure.Utilitites.UnitOfWork.UnitOfWork>();
            //services.AddScoped<ICommentService, Comments>();
        }
    }
}
