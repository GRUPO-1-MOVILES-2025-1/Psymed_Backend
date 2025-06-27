using Microsoft.EntityFrameworkCore;
using psymed_platform.Profiles.Domain.Model.Aggregates;
using psymed_platform.Profiles.Domain.Repositories;
using psymed_platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using psymed_platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace psymed_platform.Profiles.Infrastructure.Persistence.EFC.Repositories;
/// <summary>
/// Repository implementation for Profile aggregate.
/// </summary>
public class ProfileRepository(AppDbContext context): BaseRepository<Profile>(context),IProfileRepository
{
    public async Task<Profile?> FindByUserIdAsync(string userId)
    {
        return await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
    }
}