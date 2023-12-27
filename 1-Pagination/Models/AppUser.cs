using Microsoft.AspNetCore.Identity;

namespace _1_Pagination.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
    }
}
