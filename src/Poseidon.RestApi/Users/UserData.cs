namespace Poseidon.RestApi.Users
{
    public class UserData : IUserData
    {
        public int Id { get; }
        public string Username { get; }
        public string Fullname { get; }
        public string Role { get; }

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
    }
}