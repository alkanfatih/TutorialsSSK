using BlogProject.App.IRepositories;
using BlogProject.App.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Utilities.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepo AppUserRepo { get; }
        IArticleRepo ArticleRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        ICommentRepo CommentRepo { get; }

        ICategorService CategorService { get; }
    }
}
