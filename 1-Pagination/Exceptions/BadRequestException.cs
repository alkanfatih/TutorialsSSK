namespace _1_Pagination.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        public BadRequestException(string message): base(message)
        {

        }
    }
}
