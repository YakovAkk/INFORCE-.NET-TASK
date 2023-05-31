using INFORCE_.NET_TASK.DataDomain.Entities;

namespace INFORCE_.NET_TASK.Tests
{
    public class Utility
    {
        public static UserEntity GetUser()
        {
            return new UserEntity()
            {
                Id = 2,
                Name = "Test",
                PasswordHash = new byte[] { 1, 2, 3, 4, 5, 6, },
                PasswordSalt = new byte[] { 1, 1, 2, 45, 6 },
                Role = "User",
                Urls = new List<UrlEntity> {
                    new UrlEntity() {Id = 1, LongUrl = "https://google.com", UrlCode = "1234567890", CreatedDate = new DateTime(2000,10,13)}
                }
            };
        }
    }
}
