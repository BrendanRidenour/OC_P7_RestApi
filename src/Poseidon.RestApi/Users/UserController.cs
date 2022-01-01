using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;
using Poseidon.RestApi.Logins;

namespace Poseidon.RestApi.Users
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : EntityControllerHelperBase<UserEntity>
    {
        private readonly IPasswordHasher _passwordHasher;

        protected override string ReadEntityActionName => nameof(Read);

        public UserController(ICrudStore<UserEntity> crudStore,
            IPasswordHasher passwordHasher)
            : base(crudStore)
        {
            this._passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<ActionResult<UserData>> Create([FromBody] UserEntity entity)
        {
            entity.Password = this._passwordHasher.Hash(entity.Password);

            var baseResult = await base.CreateEntity(entity);

            if (baseResult.Result is not CreatedAtActionResult createdAtResult)
                throw new InvalidOperationException("Unexpected ActionResult returned from base class");

            var userData = new UserData(entity);

            return CreatedAtAction(
                actionName: createdAtResult.ActionName,
                controllerName: createdAtResult.ControllerName,
                routeValues: createdAtResult.RouteValues,
                value: userData);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserData?>> Read([FromRoute] int id)
        {
            var baseResult = await base.ReadEntity(id);

            if (baseResult.Value is null)
                return baseResult.Result!;

            return new UserData(baseResult.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ActionResult> Update([FromRoute] int id, [FromBody] UserData user)
        {
            var entity = new UserEntity()
            {
                Id = id,
                Username = user.Username,
                Fullname = user.Fullname,
                Role = user.Role,
                Password = null!,
            };

            return this.UpdateEntity(id, entity);
        }

        protected override async Task<ActionResult> UpdateEntity(int id,
            UserEntity entity)
        {
            var existingEntity = await this.CrudStore.Read(id);
            if (existingEntity is null)
                return NotFound();

            existingEntity.Id = id;
            existingEntity.Username = entity.Username;
            existingEntity.Fullname = entity.Fullname;
            existingEntity.Role = entity.Role;

            await this.CrudStore.Update(existingEntity);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<ActionResult> Delete([FromRoute] int id) => base.DeleteEntity(id);
    }
}