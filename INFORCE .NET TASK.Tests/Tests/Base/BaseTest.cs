using INFORCE_.NET_TASK.DataDomain.Database;
using INFORCE_.NET_TASK.DataDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace INFORCE_.NET_TASK.Tests.Tests.Base
{
    public class BaseTest
    {
        protected DbContextOptions<AppDBContext> options;

        public BaseTest()
        {
            options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase(GetType().Name).Options;

            using (var dbContext = CreateDbContext())
                dbContext.Database.EnsureDeleted();
        }

        protected AppDBContext CreateDbContext() => new AppDBContext(options);

        protected async void SetupEnvironmentData()
        {
            using var dbInit = CreateDbContext();

            dbInit.Users.Add(Utility.GetUser());

            var user = new UserEntity()
            {
                Id = 3,
                Name = "Test2",
                PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, },
                PasswordSalt = new byte[] { 1, 1, 2, 45, 6 },
                Role = "Admin",
            };

            dbInit.Users.Add(user);

            await dbInit.SaveChangesAsync();
        }

        protected void VerifyUser(UserEntity expectedResult, UserEntity actualResult)
        {
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
            Assert.Equal(expectedResult.PasswordHash, actualResult.PasswordHash);
            Assert.Equal(expectedResult.PasswordSalt, actualResult.PasswordSalt);

            if (expectedResult.Urls != null)
                for (int i = 0; i < expectedResult.Urls.Count; i++)
                {
                    VerifyUrls(expectedResult.Urls[i], actualResult.Urls[i]);
                }
        }

        protected void VerifyUrls(UrlEntity expectedResult, UrlEntity actualResult)
        {
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.LongUrl, actualResult.LongUrl);
            Assert.Equal(expectedResult.UrlCode, actualResult.UrlCode);
            Assert.Equal(expectedResult.CreatedDate, actualResult.CreatedDate);
        }
    }
}
