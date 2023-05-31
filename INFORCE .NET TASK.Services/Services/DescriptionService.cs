using INFORCE_.NET_TASK.DataDomain.Database;
using INFORCE_.NET_TASK.DataDomain.Entities;
using INFORCE_.NET_TASK.Services.Model.InputModel;
using INFORCE_.NET_TASK.Services.Model.ViewModel;
using INFORCE_.NET_TASK.Services.Services.Base;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace INFORCE_.NET_TASK.Services.Services
{
    public class DescriptionService : DbService<AppDBContext>, IDescriptionService
    {
        public DescriptionService(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public async Task<DescriptionViewModel> GetDescriptionAsync()
        {
            using(var dbContext = CreateDbContext())
            {
                var desc = await dbContext.Descriptions.FirstOrDefaultAsync();

                if (desc == null)
                    throw new Exception("Description doesn't exist");

                return desc.MapTo<DescriptionViewModel>();
            }
        }

        public async Task SetDescriptionAsync(DescriptionInputModel inputModel)
        {
            using (var dbContext = CreateDbContext())
            {
                var desc = await dbContext.Descriptions.FirstOrDefaultAsync();

                if (desc == null)
                    throw new Exception("Description doesn't exist");

                desc.Description = inputModel.Description;
                dbContext.Descriptions.Update(desc);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
