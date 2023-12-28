using BlogProject.Core.DomainModels.BaseModels;
using BlogProject.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.DomainModels.Models
{
    public class Category : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string Url { get { return NormalizedUrl.TurkishToEnglish(Name); } }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
