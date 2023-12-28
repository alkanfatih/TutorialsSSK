using BlogProject.App.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Services.IServices
{
    public interface IArticleService
    {
        Task<ArticleDTO> GetArticleByIdAsync(string articleId);
        Task<IEnumerable<ArticleDTO>> GetArticlesByUserIdAsync(string userId);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
        Task<int> CreateArticle(ArticleDTO articleDto);
        int UpdateArticle(ArticleDTO articleDto);
        Task<int> DeleteArticle(string articleId);
    }
}
