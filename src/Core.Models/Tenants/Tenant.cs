using System.ComponentModel.DataAnnotations;
using Core.Models.Base;

namespace Core.Models.Tenants;

public class Tenant : IHasCreationTime, IHasUpdateTime, IEntity<int>
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [MaxLength(128)]
    [Required]
    public string Domain { get; set; }

    [MaxLength(128)]
    [Required]
    public string Name { get; set; }

    [MaxLength(512)]
    [Required]
    public string Description { get; set; }
}