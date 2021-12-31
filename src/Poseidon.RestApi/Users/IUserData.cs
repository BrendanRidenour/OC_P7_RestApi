namespace Poseidon.RestApi.Users
{
    public interface IUserData
    {
        int Id { get; }
        string Username { get; }
        string Fullname { get; }
        string Role { get; }
    }
}
