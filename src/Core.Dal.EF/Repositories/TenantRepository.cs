using Core.Dal.Repositories;
using Core.Models.Tenants;

namespace Core.Dal.EF.Repositories;

public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
{
    public TenantRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}