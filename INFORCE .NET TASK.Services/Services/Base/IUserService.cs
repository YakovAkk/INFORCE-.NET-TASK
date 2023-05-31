using INFORCE_.NET_TASK.DataDomain.Entities;
using INFORCE_.NET_TASK.Services.Model;

namespace INFORCE_.NET_TASK.Services.Services.Base
{
    public interface IUserService
    {
        Task CreateAsync(UserEntity userModel);
        Task<UserEntity> GetByIdAsync(int uId);
        Task<UserEntity> GetUserByNameAsync(string name);
        Task<bool> IsUserExistsAsync(string name);
        Task<bool> IsUserHasUrlAsync(UserEntity user, string url);
    }
}
