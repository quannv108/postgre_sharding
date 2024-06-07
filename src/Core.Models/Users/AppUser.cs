using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Base;
using Core.Models.Tenants;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Users;

public class AppUser : IdentityUser<Guid>, IHasTenantId, IHasCreationTime, IHasUpdateTime, IEntity
{
    [Key]
    public int TenantId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [NotMapped]
    public int ShardKey => TenantId;
}