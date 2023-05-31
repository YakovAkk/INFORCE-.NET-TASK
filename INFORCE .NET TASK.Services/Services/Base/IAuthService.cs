using INFORCE_.NET_TASK.Services.Model.InputModel;
using INFORCE_.NET_TASK.Services.Model.ViewModel;

namespace INFORCE_.NET_TASK.Services.Services.Base
{
    public interface IAuthService
    {
        Task<TokenViewModel> LoginAsync(UserLoginInputModel inputModel);
        Task<UserViewModel> RegisterAsync(UserRegistrationInputModel inputModel);
    }
}
