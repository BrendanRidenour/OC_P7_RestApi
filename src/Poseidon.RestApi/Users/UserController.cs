using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Users
{
    public class UserController : EntityControllerBase<UserEntity>
    {
        public UserController(ICrudStore<UserEntity> crudStore)
            : base(crudStore)
        { }
    }
}