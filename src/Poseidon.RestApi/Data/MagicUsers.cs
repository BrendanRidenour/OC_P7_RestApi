using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// These "magic" users can login to the API (via the LoginController) without actually
    /// existing inside the Users table in the database. This (i) allows for easier demos,
    /// (ii) solves the chicken-and-egg problem by making it possible to perform CRUD
    /// operations against the User API endpoint before any such users exist to login with,
    /// and (iii) prevents any sensitive data (such as admin password hashes) from existing
    /// inside Entity Framework Data Migrations, which may be checked into source control.
    /// </summary>
    public class MagicUsers
    {
        public UserEntity Admin { get; }
        public UserEntity User { get; }

        public MagicUsers(UserEntity admin, UserEntity user)
        {
            Admin = admin;
            User = user;
        }
    }
}