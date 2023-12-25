using _1_Pagination.Models;

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
    }
}
