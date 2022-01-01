using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Internal;
using Poseidon.RestApi.Mocks;
using System.Threading.Tasks;
using Xunit;
using static Poseidon.RestApi.TestHelpers;

namespace Poseidon.RestApi.Users
{
    public class UserControllerTests
    {
        [Fact]
        public void HasAuthorizeAttribute()
        {
            var attribute = GetClassAttribute<UserController, AuthorizeAttribute>();

            Assert.NotNull(attribute);
        }

        [Fact]
        public void InheritsAbstractValidator()
        {
            Assert.True(typeof(EntityControllerBase<UserEntity>)
                .IsAssignableFrom(typeof(UserController)));
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreatedAtAction()
        {
            var userStore = UserStore();
            var controller = Controller(userStore);
            var user = User();
            var userData = new UserData(user);

            var actionResult = await controller.Create(user);

            var createdResult = Assert.IsType<CreatedAtActionResult>(
                actionResult.Result);
            Assert.Equal("Read", createdResult.ActionName);
            Assert.Null(createdResult.ControllerName);
            Assert.Equal(userStore.Create_Result!.Id, createdResult.RouteValues!["id"]);
            var createdUserData = Assert.IsType<UserData>(createdResult.Value);
            Assert.Equal(userData.Id, createdUserData.Id);
            Assert.Equal(userData.Username, createdUserData.Username);
            Assert.Equal(userData.Fullname, createdUserData.Fullname);
            Assert.Equal(userData.Role, createdUserData.Role);
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
        public async Task Read_BaseResultValueIsNotNull_ReturnsOkObjectResult()
        {
            var userStore = UserStore();
            var controller = Controller(userStore);

            var actionResult = await controller.Read(id: 1);

            var expectedUserData = userStore.Read_Result!;

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var okUserData = Assert.IsType<UserData>(okResult.Value);
            Assert.Equal(expectedUserData.Id, okUserData.Id);
            Assert.Equal(expectedUserData.Username, okUserData.Username);
            Assert.Equal(expectedUserData.Fullname, okUserData.Fullname);
            Assert.Equal(expectedUserData.Role, okUserData.Role);
        }

        private static MockUserEntityCrudStore UserStore() =>
            new MockUserEntityCrudStore();
        private static UserController Controller(
            MockUserEntityCrudStore? userStore = null) =>
            new UserController(userStore ?? UserStore());
        private static UserEntity User(int id = 1, string username = "Username",
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
    }
}