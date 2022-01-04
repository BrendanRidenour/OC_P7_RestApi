using Microsoft.EntityFrameworkCore;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    /// <summary>
    /// A class to perform CRUD operations against a <see cref="UserEntity"/>
    /// </summary>
    public class UserEntityCrudStore
        : EntityCrudStore<UserEntity>, IReadOperation<Username, UserEntity>
    {
        public readonly MagicUsers _magicUsers;

        /// <summary>
        /// Instantiates the class
        /// </summary>
        /// <param name="db">The Entity Framework data context to use to perform CRUD operations</param>
        /// <param name="magicUsers">The class of configurable users who do NOT exists within the Entity Framework database but who can still be queried by username during login attempts. See <see cref="MagicUsers"/> for more information.</param>
        public UserEntityCrudStore(PoseidonDbContext db, MagicUsers magicUsers)
            : base(db)
        {
            this._magicUsers = magicUsers;
        }

        /// <summary>
        /// Queries the <see cref="PoseidonDbContext"/> and <see cref="MagicUsers"/> for entities with a matching <see cref="Username"/>
        /// </summary>
        /// <param name="username">The username to use in the query</param>
        /// <returns>The <see cref="UserEntity"/> with a matching <see cref="Username"/> or null</returns>
        public async Task<UserEntity?> Read(Username username)
        {
            var un = username.Value.Trim().ToUpperInvariant();

            if (un == this._magicUsers.Admin.Username.Trim().ToUpperInvariant())
                return this._magicUsers.Admin;

            if (un == this._magicUsers.User.Username.Trim().ToUpperInvariant())
                return this._magicUsers.User;

            return await this.Db.Users.SingleOrDefaultAsync(
                u => u.Username.ToUpper() == un);
        }
    }
}