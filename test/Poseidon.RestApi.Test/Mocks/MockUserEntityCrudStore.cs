using Poseidon.RestApi.Data;
using Poseidon.RestApi.Users;
using System.Threading.Tasks;

namespace Poseidon.RestApi.Mocks
{
    public class MockUserEntityCrudStore : ICrudStore<UserEntity>
    {
        public UserEntity? Create_InputEntity;
        public UserEntity? Create_Result;
        public Task<UserEntity> Create(UserEntity entity)
        {
            this.Create_InputEntity = entity;
            if (this.Create_Result is null)
                this.Create_Result = entity;

            return Task.FromResult(this.Create_Result);
        }

        public int? Read_InputId;
        public UserEntity? Read_Result = CreateEntity();
        public Task<UserEntity?> Read(int id)
        {
            this.Read_InputId = id;

            return Task.FromResult(Read_Result);
        }

        public UserEntity? Update_InputEntity;
        public UserEntity? Update_Result;
        public Task<UserEntity> Update(UserEntity entity)
        {
            this.Update_InputEntity = entity;
            this.Update_Result = entity;

            return Task.FromResult(entity);
        }

        public int? Delete_InputId;
        public Task Delete(int id)
        {
            this.Delete_InputId = id;

            return Task.CompletedTask;
        }

        private static UserEntity CreateEntity() => new UserEntity()
        {
            Id = 1,
            Username = "Username",
            Password = "P@ssw0rd",
            Fullname = "Full Name",
            Role = "Role",
        };
    }
}