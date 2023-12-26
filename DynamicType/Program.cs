using System.Linq.Dynamic.Core;

namespace DynamicType
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person{ Id=1, Name="Fatih", Age=25, City="Istanbul" },
                new Person{ Id=2, Name="Ayşe", Age=35, City="Bursa" },
                new Person{ Id=3, Name="Zeynep", Age=35, City="Ankara" },
                new Person{ Id=4, Name="Mehmet", Age=35, City="Ankara" },
                new Person{ Id=5, Name="Mustat", Age=25, City="Istanbul" },
            };

            //Dinamik sorgu oluşturma
            var dynamicQuer = people.AsQueryable().Where("Age > 25").OrderBy("Name asc").Select("new (Name, Age)").Cast<dynamic>();

            Console.WriteLine("Dinamik Sorgu Sonuçları");
            foreach (var item in dynamicQuer)
            {
                Console.WriteLine($"{item.Name}, {item.Age}");
            }
        }
    }
}