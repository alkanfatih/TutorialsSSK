using System.ComponentModel.DataAnnotations;

namespace _1_Pagination.Models.DTOs
{
    public record UserLoginDTO
    {
        [Required(ErrorMessage ="User name is required")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "User pass is required")]
        public string Password { get; init; }
    }
}
