namespace Poseidon.RestApi.Users
{
    public class UserEntity : Internal.EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
    }
}