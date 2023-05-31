using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace INFORCE_.NET_TASK.DataDomain.Entities
{
    public class UrlEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string UrlCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation fields
        public UserEntity CreatedBy { get; set; }
    }
}
