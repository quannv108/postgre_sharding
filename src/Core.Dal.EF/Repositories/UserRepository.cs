using Core.Dal.Repositories;
using Core.Models.Users;

namespace Core.Dal.EF.Repositories;

public class UserRepository : BaseRepository<AppUser>, IAppUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}