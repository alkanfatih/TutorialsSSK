namespace RecordKullanimi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Person person1 = new Person("fatih", "alkan", 36);
            Console.WriteLine(person1.GetFullName());

            Person person2 = new Person("fatih", "alkan", 36);
            Console.WriteLine(person2.GetFullName());

            if (person1 == person2)
            {
                Console.WriteLine("Aynı");
            }
            else 
            {
                Console.WriteLine("Değil");
            }
        }
    }
}