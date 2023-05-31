using INFORCE_.NET_TASK.DataDomain.Database;
using INFORCE_.NET_TASK.DataDomain.Entities;
using INFORCE_.NET_TASK.Services.Model;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace INFORCE_.NET_TASK.Services.Services
{
    public class UserService : DbService<AppDBContext>, IUserService
    {
        public UserService(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public async Task CreateAsync(UserEntity user)
        {
            using var dbContext = CreateDbContext();
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
        public async Task<UserEntity> GetByIdAsync(int uId)
        {
            using var dbContext = CreateDbContext();

            var user = await dbContext.Users
                .Include(x => x.Urls)
                .FirstOrDefaultAsync(u => u.Id == uId);

            return user;
        }
        public async Task<UserEntity> GetUserByNameAsync(string name)
        {
            using var dbContext = CreateDbContext();
            var user = await dbContext.Users
                .Include(x => x.Urls)
                .FirstOrDefaultAsync(x => x.Name == name);

            return user;
        }
        public async Task<bool> IsUserExistsAsync(string name)
        {
            using var dbContext = CreateDbContext();

            var user = await GetUserByNameAsync(name);

            if (user == null)
                return false;
            return true;
        }
        public async Task<bool> IsUserHasUrlAsync(UserEntity user, string url)
        {
            if (user.Urls.Where(u => u.LongUrl == url).Any())
                return true;
            return false;
        }
    }
}
