namespace _1_Pagination.RequestFeatures
{
    public class PersonParametres : RequestParameters
    {
        //Filtreleme Konusunda Eklendi.
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = 100;

        public bool ValidAgeRange => MaxAge> MinAge;

        //Arama Konsunda Eklendi
        public string? SearchTerm { get; set; }
    }
}
