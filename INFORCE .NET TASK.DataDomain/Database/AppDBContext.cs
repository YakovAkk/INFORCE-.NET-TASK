using INFORCE_.NET_TASK.DataDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace INFORCE_.NET_TASK.DataDomain.Database
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UrlEntity> Urls { get; set; }
        public DbSet<AlgoritmDescriptionEntity> Descriptions { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var admin = new UserEntity()
            {
                Id = 1,
                Name = "admin",
                Role = "Admin"
            };

            using (var hmac = new HMACSHA512())
            {
                admin.PasswordSalt = hmac.Key;
                admin.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin"));
            }

            modelBuilder.Entity<UserEntity>().HasData(admin);

            var desc = new AlgoritmDescriptionEntity()
            {
                Id = 1,
                Description = "The algorithm for creating short URLs is as follows: I generate a new GUID and associate it with the full link provided by the user. " +
                "When I need to retrieve the link, I search for it in the database, and if it exists, I provide the URL to the user."
            };

            modelBuilder.Entity<AlgoritmDescriptionEntity>().HasData(desc);

            base.OnModelCreating(modelBuilder);
        }

    }
}
