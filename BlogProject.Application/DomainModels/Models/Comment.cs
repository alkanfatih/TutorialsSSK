using BlogProject.Core.DomainModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.DomainModels.Models
{
    public class Comment : BaseEntity, IBaseEntity
    {
        public string Content { get; set; }

        //Navigation Prop
        public string UserId { get; set; }
        public string ArticleId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Article Article { get; set; }
    }
}
