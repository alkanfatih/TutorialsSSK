using _1_Pagination.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace _1_Pagination.Extensions
{
    public static class PersonExtensions
    {
        public static IQueryable<Person> FilterPersons(IQueryable<Person> persons, uint minAge, uint maxAge) 
        { 
            return persons.Where(p => p.Age >= minAge && p.Age <= maxAge);
        }

        public static IQueryable<Person> Search(this IQueryable<Person> persons, string searchTerm) 
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return persons;

            var lowerCaseTerms = searchTerm.Trim().ToLower();

            return persons.Where(p => p.Name.ToLower().Contains(lowerCaseTerms));
        }

        //api/persons?OrderBy=Id, name desc
        public static IQueryable<Person> Sort(IQueryable<Person> persons, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return persons.OrderBy(p => p.Id);

            //Gelen querystringi parçalamak. ?orderBy=name, age desc
            var ordereredParams = orderByQueryString.Trim().Split(',');

            //Reflaction propertylerin bilgilerini elde edeceğiz (Id, Name, Age)
            var propertyInfos = typeof(Person).GetProperties(BindingFlags.Public | BindingFlags.Instance); //public üyeleri yada instance'ları istiyorum.

            var orderQueryBuilde = new StringBuilder();

            foreach ( var param in ordereredParams) 
            {
                //System.Linq.Dynamic.Core
                if (string.IsNullOrEmpty(param))
                {
                    //Bir null olma durumu olursa bir sonraki elemana geç
                    continue;
                }

                //Boşluğa göre split uyguladım
                var propertyFromQueryNam = param.Split(' ')[0];

                //Nesne üzeriden verileri
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryNam, StringComparison.InvariantCultureIgnoreCase)); //Büyük küçük harf ayrımı.

                if (objectProperty is null)
                {
                    continue;
                }

                //Arama yönüne bak.
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                //name asceding, age descending
                orderQueryBuilde.Append($"{objectProperty.Name.ToString()} {direction}");
            }

            var orderQuery = orderQueryBuilde.ToString().TrimEnd(',', ' ');

            if (orderQuery is null)
                return persons.OrderBy(b => b.Id);

            return persons.OrderBy(orderQuery);
        }
    }
}
