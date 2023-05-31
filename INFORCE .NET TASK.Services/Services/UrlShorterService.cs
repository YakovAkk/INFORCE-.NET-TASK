using INFORCE_.NET_TASK.DataDomain.Database;
using INFORCE_.NET_TASK.DataDomain.Entities;
using INFORCE_.NET_TASK.Services.Model.DTO;
using INFORCE_.NET_TASK.Services.Model.ViewModel;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace INFORCE_.NET_TASK.Services.Services
{
    public class UrlShorterService : DbService<AppDBContext>, IUrlShorterService
    {
        private readonly IUserService _userService;
        public UrlShorterService(DbContextOptions<AppDBContext> dbContextOptions, IUserService userService) : base(dbContextOptions)
        {
            _userService = userService;
        }

        public async Task CreateShortUrlAsync(UrlInputModel inputModel, string userId)
        {
            if (!Uri.TryCreate(inputModel.Url, UriKind.Absolute, out var inputUri))
                throw new InvalidOperationException("URL is invalid.");

            var uId = Convert.ToInt32(userId);

            var user = await _userService.GetByIdAsync(uId);
            if (user == null)
                throw new Exception("User doesn't exist!");

            if(await _userService.IsUserHasUrlAsync(user, inputModel.Url))
                throw new Exception("User has already the Url!");

            var url = new UrlEntity()
            {
                LongUrl = inputUri.ToString(),
                UrlCode = $"{Guid.NewGuid()}",
                CreatedBy = user,
            };

            user.Urls.Add(url);

            using (var dbContext = CreateDbContext())
            {
                dbContext.Users.Update(user);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteUrlAsync(string userId, string url)
        {
            var uId = Convert.ToInt32(userId);
            var user = await _userService.GetByIdAsync(uId);
            if (user == null)
                throw new Exception("User doesn't exist!");

            using(var dbContext = CreateDbContext())
            {
                UrlEntity urlEntity;
                if (user.Role == "Admin")
                {
                    urlEntity = await dbContext.Urls.Where(x => x.UrlCode == url).FirstOrDefaultAsync();
                    if (urlEntity == null)
                        throw new Exception("The url doesn't exist!");
                }
                else
                {
                    urlEntity = user.Urls.Where(x => x.UrlCode == url).FirstOrDefault();
                    if (urlEntity == null)
                        throw new Exception("The user doesn't have the url!");
                }

                dbContext.Urls.Remove(urlEntity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<UrlViewModel>> GetAllAsync(HttpContext context)
        {
            List<UrlEntity> urls;

            using (var dbContext = CreateDbContext())
            {
                urls = await dbContext.Urls.Include(x => x.CreatedBy).ToListAsync();
            }

            foreach (var item in urls)
            {
                item.CreatedBy.CleanUrls();
            }

            var result = urls.Select(url => url.MapTo<UrlViewModel>()).ToList();
            foreach (var item in result)
            {
                item.Host = $"{context.Request.Scheme}://{context.Request.Host}/";
            }

            return result;
        }

        public async Task<UrlViewModel> GetByIdAsync(string id)
        {
            var IsCanParse = int.TryParse(id, out var urlId);
            if (!IsCanParse)
                throw new ArgumentException("Id in incorrect!");

            using (var dbContext = CreateDbContext())
            {
                var data = await dbContext.Urls
                    .Include(x => x.CreatedBy)
                    .FirstOrDefaultAsync(x => x.Id == urlId);

                if (data == null)
                    throw new Exception("The url doesn't exist!");

                return data.MapTo<UrlViewModel>();
            }
        }
    }
}
