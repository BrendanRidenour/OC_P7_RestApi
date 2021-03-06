using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Users
{
    public class UserControllerTests
    {
        [Fact]
        public void InheritsEntityControllerHelperBase()
        {
            Assert.True(typeof(EntityControllerHelperBase<UserEntity>)
                .IsAssignableFrom(typeof(UserController)));
        }

        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<UserController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("Admin", attribute.Roles);
        }

        [Fact]
        public void HasApiControllerAttribute()
        {
            var attribute = GetClassAttribute<UserController, ApiControllerAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void HasRouteAttribute()
        {
            var attribute = GetClassAttribute<UserController, RouteAttribute>();

            Assert.NotNull(attribute);
            Assert.Equal("[controller]", attribute.Template);
        }

        [Fact]
        public void Create_HasHttpPostAttribute()
        {
            var attribute = GetMethodAttribute<UserController, HttpPostAttribute>(
                "Create");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Create_EntityParameterHasFromBodyAttribute()
        {
            var attribute = GetParameterAttribute<UserController, FromBodyAttribute>(
                "Create", "entity");

            Assert.NotNull(attribute);
        }

        [Theory]
        [InlineData("P@ssw0rd1")]
        [InlineData("P@ssw0rd2")]
        public async Task Create_WhenCalled_CallsHashOnPasswordHasher(string password)
        {
            var passwordHasher = PasswordHasher();
            var userStore = UserStore();
            var controller = Controller(userStore, passwordHasher);
            var user = UserEntity(password: password);

            await controller.Create(user);

            Assert.Equal(password, passwordHasher.Hash_InputPassword);
            Assert.Equal(passwordHasher.Hash_Result,
                userStore.Create_InputEntity!.Password);
            Assert.Equal(user, userStore.Create_InputEntity);
        }

        [Fact]
        public async Task Create_WhenBaseCreateEntityReturnsNotCreatedAtActionResult_Throws()
        {
            var controller = new TestUserController(UserStore(), PasswordHasher());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await controller.Create(UserEntity());
            });

            Assert.Equal($"Unexpected ActionResult returned from CreateEntity method: 'NotFoundResult'",
                exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Create_WhenCalled_ReturnsCreatedAtAction(int createdId)
        {
            var userStore = UserStore();
            userStore.Create_Result = UserEntity(id: createdId);
            var controller = Controller(userStore);
            var user = UserEntity();
            var userData = new UserData(user);

            var actionResult = await controller.Create(user);

            var createdResult = Assert.IsType<CreatedAtActionResult>(
                actionResult.Result);
            Assert.Equal("Read", createdResult.ActionName);
            Assert.Null(createdResult.ControllerName);
            Assert.Equal(createdId, createdResult.RouteValues!["id"]);
            var createdUserData = Assert.IsType<UserData>(createdResult.Value);
            Assert.Equal(createdId, createdUserData.Id);
            Assert.Equal(userData.Username, createdUserData.Username);
            Assert.Equal(userData.Fullname, createdUserData.Fullname);
            Assert.Equal(userData.Role, createdUserData.Role);
        }

        [Fact]
        public void ReadList_HasHttpGetAttribute()
        {
            var attribute = GetMethodAttribute<UserController, HttpGetAttribute>(
                "Read", methodIndex: 0);

            Assert.NotNull(attribute);
        }

        [Fact]
        public void ReadList_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<UserController, RouteAttribute>(
                "Read", methodIndex: 0);

            Assert.NotNull(attribute);
            Assert.Equal(string.Empty, attribute.Template);
        }

        [Fact]
        public async Task ReadList_BaseResultValueIsNull_ReturnsEmptyArray()
        {
            var userStore = UserStore();
            userStore.ReadList_Result = null!;
            var controller = Controller(userStore);

            var result = await controller.Read();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReadList_WhenCalled_ReturnsData()
        {
            var userStore = UserStore();
            var entity1 = UserEntity(id: 1);
            var entity2 = UserEntity(id: 2);
            userStore.ReadList_Result = new List<UserEntity>() { entity1, entity2 };
            var controller = Controller(userStore);

            var result = await controller.Read();

            var data1 = result.ElementAt(0);
            var data2 = result.ElementAt(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            Assert.Equal(entity1.Id, data1.Id);
            Assert.Equal(entity1.Username, data1.Username);
            Assert.Equal(entity1.Fullname, data1.Fullname);
            Assert.Equal(entity1.Role, data1.Role);

            Assert.Equal(entity2.Id, data2.Id);
            Assert.Equal(entity2.Username, data2.Username);
            Assert.Equal(entity2.Fullname, data2.Fullname);
            Assert.Equal(entity2.Role, data2.Role);
        }

        [Fact]
        public void Read_HasHttpGetAttribute()
        {
            var attribute = GetMethodAttribute<UserController, HttpGetAttribute>(
                "Read", methodIndex: 1);

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Read_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<UserController, RouteAttribute>(
                "Read", methodIndex: 1);

            Assert.NotNull(attribute);
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Read_IdParameterHasFromRouteAttribute()
        {
            var attribute = GetParameterAttribute<UserController, FromRouteAttribute>(
                "Read", "id", methodIndex: 1);

            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task Read_BaseResultValueIsNull_ReturnsBaseResult()
        {
            var userStore = UserStore();
            userStore.Read_Result = null;
            var controller = Controller(userStore);

            var actionResult = await controller.Read(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Read_BaseResultValueIsNotNull_ReturnsUserData()
        {
            var userStore = UserStore();
            var controller = Controller(userStore);

            var actionResult = await controller.Read(id: 1);

            var expectedUserData = userStore.Read_Result!;

            var userData = Assert.IsType<UserData>(actionResult.Value);
            Assert.Equal(expectedUserData.Id, userData.Id);
            Assert.Equal(expectedUserData.Username, userData.Username);
            Assert.Equal(expectedUserData.Fullname, userData.Fullname);
            Assert.Equal(expectedUserData.Role, userData.Role);
        }

        [Fact]
        public void Update_HasHttpPutAttribute()
        {
            var attribute = GetMethodAttribute<UserController, HttpPutAttribute>(
                "Update");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Update_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<UserController, RouteAttribute>(
                "Update");

            Assert.NotNull(attribute);
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Update_IdParameterHasFromRouteAttribute()
        {
            var attribute = GetParameterAttribute<UserController, FromRouteAttribute>(
                "Update", "id");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Update_EntityParameterHasFromBodyAttribute()
        {
            var attribute = GetParameterAttribute<UserController, FromBodyAttribute>(
                "Update", "user");

            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task Update_CallToReadOnCrudStoreReturnsNull_ReturnsNotFound()
        {
            var crudStore = UserStore();
            crudStore.Read_Result = null;
            var controller = Controller(crudStore);
            var data = UserData();

            var actionResult = await controller.Update(id: 1, data);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Theory]
        [InlineData(1, "password1")]
        [InlineData(2, "password2")]
        public async Task Update_WhenCalled_CallsUpdateOnCrudStoreWithCorrectId(int id,
            string existingPassword)
        {
            var crudStore = UserStore();
            crudStore.Read_Result!.Id = id;
            crudStore.Read_Result.Password = existingPassword;
            var controller = Controller(crudStore);
            var data = UserData(id: 0);

            await controller.Update(id, data);

            Assert.Equal(id, crudStore.Update_InputEntity!.Id);
            Assert.Equal(data.Username, crudStore.Update_InputEntity.Username);
            Assert.Equal(existingPassword, crudStore.Update_InputEntity.Password);
            Assert.Equal(data.Fullname, crudStore.Update_InputEntity.Fullname);
            Assert.Equal(data.Role, crudStore.Update_InputEntity.Role);
            Assert.Equal(id, crudStore.Update_InputEntity!.Id);
        }

        [Fact]
        public async Task Update_WhenCalled_ReturnsNoContent()
        {
            var crudStore = UserStore();
            var controller = Controller(crudStore);
            var data = UserData();

            var result = await controller.Update(id: 1, data);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_HasHttpDeleteAttribute()
        {
            var attribute = GetMethodAttribute<UserController, HttpDeleteAttribute>(
                "Delete");

            Assert.NotNull(attribute);
        }

        [Fact]
        public void Delete_HasRouteAttribute()
        {
            var attribute = GetMethodAttribute<UserController, RouteAttribute>(
                "Delete");

            Assert.NotNull(attribute);
            Assert.Equal("{id}", attribute.Template);
        }

        [Fact]
        public void Delete_IdParameterHasFromRouteAttribute()
        {
            var attribute = GetParameterAttribute<UserController, FromRouteAttribute>(
                "Delete", "id");

            Assert.NotNull(attribute);
        }

        [Fact]
        public async Task Delete_ReadOnCrudStoreReturnsNull_ReturnsNotFound()
        {
            var crudStore = UserStore();
            crudStore.Read_Result = null;
            var controller = Controller(crudStore);

            var result = await controller.Delete(id: 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Delete_WhenCalled_CallsDeleteOnCrudStore(int id)
        {
            var crudStore = UserStore();
            var controller = Controller(crudStore);

            await controller.Delete(id);

            Assert.Equal(id, crudStore.Delete_InputId);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsNoContent()
        {
            var crudStore = UserStore();
            var controller = Controller(crudStore);

            var result = await controller.Delete(id: 1);

            Assert.IsType<NoContentResult>(result);
        }

        private static MockPasswordHasher PasswordHasher() => new MockPasswordHasher();
        private static MockUserEntityCrudStore UserStore() =>
            new MockUserEntityCrudStore();
        private static UserController Controller(
            MockUserEntityCrudStore? userStore = null,
            MockPasswordHasher? passwordHasher = null) =>
            new UserController(userStore ?? UserStore(),
                passwordHasher ?? PasswordHasher());
        private static UserData UserData(int id = 1, string username = "Username",
            string fullname = "Full Name", string role = "Role") =>
            new UserData(id, username, fullname, role);
        private static UserEntity UserEntity(int id = 1, string username = "Username",
            string password = "P@ssw0rd", string fullname = "Full Name",
            string role = "Role") =>
            new UserEntity()
            {
                Id = id,
                Username = username,
                Password = password,
                Fullname = fullname,
                Role = role,
            };
        private class TestUserController : UserController
        {
            public TestUserController(ICrudStore<UserEntity> crudStore, IPasswordHasher passwordHasher)
                : base(crudStore, passwordHasher)
            { }

            protected override Task<ActionResult<UserEntity>> CreateEntity(UserEntity entity) =>
                Task.FromResult<ActionResult<UserEntity>>(NotFound());
        }
    }
}