using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Poseidon.RestApi.Internal
{
    public abstract class EntityBase
    {
        [BindNever]
        public virtual int Id { get; set; }
    }
}