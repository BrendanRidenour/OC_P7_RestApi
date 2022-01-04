using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Poseidon.RestApi.Internal
{
    /// <summary>
    /// A base class for use by entities
    /// </summary>
    public abstract class EntityBase
    {
        [BindNever]
        public virtual int Id { get; set; }
    }
}