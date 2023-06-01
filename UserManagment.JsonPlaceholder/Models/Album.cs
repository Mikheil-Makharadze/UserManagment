namespace JsonPlaceholder.Models
{
    public class Album
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string title { get; set; }

        public ICollection<Photo>? Photos { get; set; }
    }
}
