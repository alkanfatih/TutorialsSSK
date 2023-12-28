using AutoMapper;
using BlogProject.App.DTOs;
using BlogProject.App.Exceptions;
using BlogProject.App.Services.IServices;
using BlogProject.App.Utilities.ILogging;
using BlogProject.App.Utilities.IUnitOfWorks;
using BlogProject.Core.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> CreateArticle(ArticleDTO articleDto)
        {
            try
            {
                var article = _mapper.Map<Article>(articleDto);
                return await _unitOfWork.ArticleRepo.AddAsync(article);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"{articleDto.Title} başlıklı makale eklenirken hata oluştu", ex);
                throw new MyException("Hata Gerçekleşti");
            }
        }

        public async Task<int> DeleteArticle(string articleId)
        {
            var article = await _unitOfWork.ArticleRepo.GetByIdAsync(articleId);
            if (article != null)
                article.UpdateDate = DateTime.Now;
                article.Status = Core.DomainModels.Enums.EntityStatus.Deleted;
            return _unitOfWork.ArticleRepo.Delete(article);
            return 0;
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            var articles = await _unitOfWork.ArticleRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }

        public async Task<ArticleDTO> GetArticleByIdAsync(string articleId)
        {
            var article = await _unitOfWork.ArticleRepo.GetByIdAsync(articleId);
            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticlesByUserIdAsync(string userId)
        {
            var articles = await _unitOfWork.ArticleRepo.GetAllAsync(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }

        public int UpdateArticle(ArticleDTO articleDto)
        {
            var article = _mapper.Map<Article>(articleDto);
            article.UpdateDate = DateTime.Now;
            article.Status = Core.DomainModels.Enums.EntityStatus.Updated;
            return _unitOfWork.ArticleRepo.Update(article);
        }
    }
}
