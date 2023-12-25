namespace _1_Pagination.Exceptions
{
    public class AgeOutOfRangeBadRequestException : BadRequestException
    {
        public AgeOutOfRangeBadRequestException() : base("Maximum age should be less than 100 and than 10")
        {
        }
    }
}
