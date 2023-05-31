using INFORCE_.NET_TASK.Services.Model.DTO;
using INFORCE_.NET_TASK.Services.Model.ViewModel;
using Microsoft.AspNetCore.Http;

namespace INFORCE_.NET_TASK.Services.Services.Base
{
    public interface IUrlShorterService
    {
        Task CreateShortUrlAsync(UrlInputModel inputModel, string userId);
        Task DeleteUrlAsync(string userId, string url);
        Task<List<UrlViewModel>> GetAllAsync(HttpContext context);
        Task<UrlViewModel> GetByIdAsync(string id);
    }
}
