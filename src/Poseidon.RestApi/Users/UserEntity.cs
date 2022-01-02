using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Users
{
    public class UserEntity : Internal.EntityBase, IUserData
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Fullname { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }
}