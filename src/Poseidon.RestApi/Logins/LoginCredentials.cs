using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Logins
{
    /// <summary>
    /// Represents a <see cref="LoginCredentials" /> model class
    /// </summary>
    public class LoginCredentials
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}