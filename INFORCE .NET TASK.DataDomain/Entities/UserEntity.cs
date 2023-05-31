using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace INFORCE_.NET_TASK.DataDomain.Entities
{
    public partial class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }

        // Navigation fields
        public List<UrlEntity> Urls { get; set; }

        public UserEntity()
        {
            Urls = new List<UrlEntity>();
        }

    }
    public partial class UserEntity
    {
        public void CleanUrls()
        {
            Urls = null;
        }
    }

}
