﻿using System.ComponentModel.DataAnnotations;

namespace Poseidon.RestApi.Rules
{
    public class RuleEntity : Internal.EntityBase
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Json { get; set; }
        public string? Template { get; set; }
        public string? SqlStr { get; set; }
        public string? SqlPart { get; set; }
    }
}