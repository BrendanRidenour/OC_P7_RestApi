using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Internal;

namespace Poseidon.RestApi.Users
{
    [Authorize]
    public class UserController : EntityControllerBase<UserEntity>
    {
        public UserController(ICrudStore<UserEntity> crudStore)
            : base(crudStore)
        { }

        public override async Task<ActionResult<UserEntity>> Create(
            [FromBody] UserEntity entity)
        {
            var baseResult = await base.Create(entity);

            if (baseResult.Result is not CreatedAtActionResult createdAtResult)
                return baseResult;

            var userData = new UserData(entity);

            return CreatedAtAction(
                actionName: createdAtResult.ActionName,
                controllerName: createdAtResult.ControllerName,
                routeValues: createdAtResult.RouteValues,
                value: userData);
        }

        public override async Task<ActionResult<UserEntity?>> Read([FromRoute] int id)
        {
            var baseResult = await base.Read(id);

            if (baseResult.Value is null)
                return baseResult;

            var userData = new UserData(baseResult.Value);

            return Ok(userData);
        }
    }
}