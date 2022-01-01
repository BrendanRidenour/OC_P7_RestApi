namespace Poseidon.RestApi.Users
{
    public class UserData : IUserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UserData() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}