using INFORCE_.NET_TASK.Services.Model.InputModel;
using INFORCE_.NET_TASK.Services.Model.ViewModel;

namespace INFORCE_.NET_TASK.Services.Services.Base
{
    public interface IDescriptionService
    {
        Task<DescriptionViewModel> GetDescriptionAsync();
        Task SetDescriptionAsync(DescriptionInputModel inputModel);
    }
}
