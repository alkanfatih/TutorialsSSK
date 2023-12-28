using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.DomainServices
{
    public class NormalizedUrl
    {
        public static string TurkishToEnglish(string name)
        {
            string turkishCharcter = "ığüşöç ";
            string englishCharcter = "igusoc-";

            for (int i = 0; i < turkishCharcter.Length; i++)
            {
                name = name.ToLower().Replace(turkishCharcter[i], englishCharcter[i]);
            }

            return name;
        }
    }
}
