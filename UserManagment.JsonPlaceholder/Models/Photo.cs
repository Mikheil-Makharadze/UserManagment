namespace JsonPlaceholder.Models
{
    public class Photo
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        
        //Realationship
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
