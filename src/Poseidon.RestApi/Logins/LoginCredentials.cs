using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Logins
{
    public class LoginCredentials
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}