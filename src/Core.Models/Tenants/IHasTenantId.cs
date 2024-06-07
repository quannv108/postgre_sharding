using Core.Models.Base;

namespace Core.Models.Tenants;

public interface IHasTenantId : IShardEntity<int>
{
    int TenantId { get; }
}