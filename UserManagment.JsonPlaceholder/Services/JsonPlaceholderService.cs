using JsonPlaceholder.Interfaces;
using JsonPlaceholder.Models;

namespace JsonPlaceholder.Services
{
    public class JsonPlaceholderService :HttpBaseService, IJsonPlaceholderService
    {
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        public JsonPlaceholderService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<ToDo>> GetUserTodos(int userId)
        {
            return await GetItems<ToDo>(baseUrl + $"users/{userId}/todos"); 
        }

        public async Task<List<Album>> GetUserAlbums(int userId)
        {
            var albums = await GetItems<Album>(baseUrl + $"users/{userId}/albums");

            // Retrieve Photos for each albom
            foreach (var album in albums)
            {
                album.Photos = await GetAlbumsPhotots(album.Id);
            }

            return albums;
        }

        public async Task<List<Post>> GetUserPosts(int userId)
        {
            var posts = await GetItems<Post>(baseUrl + $"users/{userId}/posts");

            // Retrieve comments for each post
            foreach (var post in posts)
            {
                post.Comments = await GetPostComments(post.Id);
            }

            return posts;
        }        

        private async Task<List<Photo>> GetAlbumsPhotots(int albumId)
        {
            return await GetItems<Photo>(baseUrl + $"albums/{albumId}/photos");
        }

        private async Task<List<Comment>> GetPostComments(int postId)
        {
            return await GetItems<Comment>(baseUrl + $"posts/{postId}/comments");
        }
    }
}
