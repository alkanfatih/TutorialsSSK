using BlogProject.Core.DomainModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.DomainModels.Models
{
    public class Article : BaseEntity, IBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        //Navgitaion Prop
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
