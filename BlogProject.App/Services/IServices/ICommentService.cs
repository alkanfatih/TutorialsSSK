using BlogProject.App.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Services.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetCommentsByArticleAsync(string articleId);
        Task<int> CreateCommentAsync(CommentDTO commentDto);
        int UpdateComment(CommentDTO commentDto);
        Task<int> DeleteCommentAsync(string commentId);
    }
}
