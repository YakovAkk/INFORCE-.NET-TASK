using INFORCE_.NET_TASK.Services.Services;
using INFORCE_.NET_TASK.Tests.Tests.Base;
using INFORCE_.NET_TASK.DataDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace INFORCE_.NET_TASK.Tests.Tests
{
    public class UserServiceTests : BaseTest
    {
        private UserService Service;
        
        public UserServiceTests()
        {
            Service = new UserService(options);
        }

        [Fact]
        public async void CreateAsyncTestRegularCase()
        {
            //Arrange
            var expectedResult = new UserEntity()
            {
                Id = 2,
                Name = "Test2",
                PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, },
                PasswordSalt = new byte[] { 1, 1, 2, 45, 6 },
                Role = "Admin",
            };

            //Act
            await Service.CreateAsync(expectedResult);

            //Assert
            using var db = CreateDbContext();

            var actualResult = await db.Users
                .Include(x => x.Urls)
                .FirstOrDefaultAsync(x => x.Id == expectedResult.Id);

            Assert.NotNull(actualResult);
            VerifyUser(expectedResult, actualResult);
        }

        [Fact]
        public async void GetUserByNameAsyncTestRegularCase()
        {
            //Arrange
            SetupEnvironmentData();
            var expectedResult = Utility.GetUser();

            //Act
            var actualResult = await Service.GetUserByNameAsync("Test");

            //Assert
            Assert.NotNull(actualResult);
            VerifyUser(expectedResult, actualResult);
        }

        [Fact]
        public async void IsUserExistsAsyncTestRegularCase()
        {
            //Arrange
            SetupEnvironmentData();

            //Act
            var actualResult = await Service.IsUserExistsAsync("Test");

            //Assert
            Assert.True(actualResult);
        }

        public static IEnumerable<object[]> DataForUserHasUrlMethod =>
           new List<object[]>
           {
                new object[]
                {
                    Utility.GetUser(),
                     "https://google.com",
                     true

                },
                new object[]
                {
                    new UserEntity()
                    {
                        Id = 3,
                        Name = "Test2",
                        PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, },
                        PasswordSalt = new byte[] { 1, 1, 2, 45, 6 },
                        Role = "Admin",
                    },
                    "https://google.com",
                    false
                },
           };
        [Theory]
        [MemberData(nameof(DataForUserHasUrlMethod))]
        public async void IsUserHasUrlAsyncTestRegularCase(UserEntity user, string url, bool expectedResult)
        {
            //Arrange
            SetupEnvironmentData();

            //Act
            var actualResult = await Service.IsUserHasUrlAsync(user, url);

            //Assert
            Assert.Equal(expectedResult,actualResult);
        }
    }
}
