using Poseidon.RestApi.Internal;
using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Rules
{
    public class RuleEntity : EntityBase, IEntityBasePropertyCopy<RuleEntity>
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Json { get; set; }
        public string? Template { get; set; }
        public string? SqlStr { get; set; }
        public string? SqlPart { get; set; }

        public void CopyProperties(RuleEntity entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            Json = entity.Json;
            Template = entity.Template;
            SqlStr = entity.SqlStr;
            SqlPart = entity.SqlPart;
        }
    }
}