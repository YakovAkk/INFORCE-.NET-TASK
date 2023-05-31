namespace INFORCE_.NET_TASK.Services.Model.ViewModel
{
    public class UrlViewModel
    {
        public int Id { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string UrlCode { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public UserViewModel CreatedBy { get; set; }
    }
}
