using BlogProject.App.IRepositories;
using BlogProject.Core.DomainModels.Models;
using BlogProject.Infrastructure.Contexts;
using BlogProject.Infrastructure.Repositories.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Repositories
{
    public class CommentRepo : BaseRepo<Comment>, ICommentRepo
    {
        public CommentRepo(AppDbContext context) : base(context)
        {
        }
    }
}
