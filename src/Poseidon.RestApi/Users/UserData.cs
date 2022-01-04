namespace Poseidon.RestApi.Users
{
    /// <summary>
    /// Represents a <see cref="UserData" /> model class
    /// </summary>
    public class UserData : IUserData
    {
        public int Id { get; set; } = default;
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Role { get; set; } = null!;

        /// <summary>
        /// Instantiates the class by coping the properties of the given <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">The entity whose properties to copy onto this instance</param>
        public UserData(UserEntity entity)
            : this(entity.Id, entity.Username, entity.Fullname, entity.Role)
        { }

        /// <summary>
        /// Instantiates the class using the given arguments
        /// </summary>
        public UserData(int id, string username, string fullname, string role)
            : this()
        {
            this.Id = id;
            this.Username = username;
            this.Fullname = fullname;
            this.Role = role;
        }

        /// <summary>
        /// Instantiates the class
        /// </summary>
        public UserData() { }
    }
}