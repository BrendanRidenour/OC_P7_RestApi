using Microsoft.EntityFrameworkCore;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;

namespace Poseidon.RestApi.Data
{
    public class UserEntityCrudStore
        : EntityCrudStore<UserEntity>, IReadOperation<Username, UserEntity>
    {
        public readonly MagicUsers _magicUsers;

        public UserEntityCrudStore(PoseidonDbContext db, MagicUsers magicUsers)
            : base(db)
        {
            this._magicUsers = magicUsers;
        }

        public async Task<UserEntity?> Read(Username id)
        {
            var username = id.Value.Trim().ToUpperInvariant();

            if (username == this._magicUsers.Admin.Username.Trim().ToUpperInvariant())
                return this._magicUsers.Admin;

            if (username == this._magicUsers.User.Username.Trim().ToUpperInvariant())
                return this._magicUsers.User;

            return await this.Db.Users.SingleOrDefaultAsync(
                u => u.Username.ToUpper() == username);
        }
    }
}