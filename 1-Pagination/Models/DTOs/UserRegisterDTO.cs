using System.ComponentModel.DataAnnotations;

namespace _1_Pagination.Models.DTOs
{
    public record UserRegisterDTO
    {
        /// <summary>
        /// Adınız
        /// </summary>
        public string? FirstName { get; init; }
        /// <summary>
        /// Soyadınız
        /// </summary>
        public string? LastName { get; init; }
        /// <summary>
        /// Kullanici Adi
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }
        /// <summary>
        /// Şifre en az 8 harf büyük küçük harf
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Role { get; init; }
    }
}
