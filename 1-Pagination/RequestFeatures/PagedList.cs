using _1_Pagination.Models;

namespace _1_Pagination.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                PageSize = pageSize,
                TotalCount = count,
                CurrentPage = pageNumber,
                TotalPage = (int)Math.Ceiling(count / (decimal)pageSize)
            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize) 
        { 
            var count = source.Count();
            var items = source.Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
