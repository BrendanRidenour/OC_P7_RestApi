using Poseidon.RestApi.Data;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Users;
using System.Threading.Tasks;

namespace Poseidon.RestApi.Mocks
{
    public class MockUserEntityReadOperation : IReadOperation<Username, UserEntity>
    {
        public Username? Read_InputId;
        public UserEntity? Read_Result = new UserEntity()
        {
            Id = 1,
            Username = "Username",
            Password = "P@ssw0rd",
            Fullname = "Full Name",
            Role = "Role",
        };
        public Task<UserEntity?> Read(Username id)
        {
            this.Read_InputId = id;

            return Task.FromResult(this.Read_Result);
        }
    }
}