using Poseidon.RestApi.Internal;
using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Users
{
    public class UserEntity : EntityBase, IUserData, IEntityBasePropertyCopy<UserEntity>
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Fullname { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;

        public void CopyProperties(UserEntity entity)
        {
            Username = entity.Username;
            Password = entity.Password;
            Fullname = entity.Fullname;
            Role = entity.Role;
        }
    }
}