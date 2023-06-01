using JsonPlaceholder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JsonPlaceholder.Interfaces
{
    public interface IJsonPlaceholderService
    {
        Task<List<Post>> GetUserPosts(int userId);
        Task<List<Album>> GetUserAlbums(int userId);
        Task<List<ToDo>> GetUserTodos(int userId);
    }
}
