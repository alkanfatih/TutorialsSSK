using BlogProject.App.IRepositories;
using BlogProject.App.Services;
using BlogProject.App.Services.IServices;
using BlogProject.App.Utilities.IUnitOfWorks;
using BlogProject.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Utilitites.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext contet, IAppUserRepo appUserRepo, IArticleRepo articleRepo, ICategoryRepo categoryRepo, ICommentRepo commentRepo, IAppUserService appUserService, ICategorService categorService, IArticleService articleService)
        {
            _context = contet;
            AppUserRepo = appUserRepo;
            ArticleRepo = articleRepo;
            CategoryRepo = categoryRepo;
            CommentRepo = commentRepo;
            AppUserService = appUserService;
            CategorService = categorService;
            ArticleService = articleService;
        }

        public IAppUserRepo AppUserRepo { get; }

        public IArticleRepo ArticleRepo { get; }

        public ICategoryRepo CategoryRepo {get;}

        public ICommentRepo CommentRepo {get;}

        public ICategorService CategorService { get; }

        public IAppUserService AppUserService { get; }

        public IArticleService ArticleService { get; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
