namespace Poseidon.RestApi.Users
{
    public class UserData : IUserData
    {
        public int Id { get; set; } = default;
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Role { get; set; } = null!;

        public UserData(UserEntity entity)
            : this(entity.Id, entity.Username, entity.Fullname, entity.Role)
        { }

        public UserData(int id, string username, string fullname, string role)
        {
            this.Id = id;
            this.Username = username;
            this.Fullname = fullname;
            this.Role = role;
        }

        public UserData() { }
    }
}