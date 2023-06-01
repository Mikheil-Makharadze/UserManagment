namespace JsonPlaceholder.Models
{
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string title { get; set; }
        public string body { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
