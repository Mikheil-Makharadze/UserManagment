using JsonPlaceholder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagment.API.Controllers;

namespace API.Controllers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class JsonPlaceholderController : BaseAPIController
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;
        public JsonPlaceholderController(IJsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        [HttpGet("ToDo{UserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserToDos(int UserId)
        {
            var list = await _jsonPlaceholderService.GetUserTodos(UserId);
            if (list == null || !list.Any())
            {
                return NotFound("could not find any ToDo of the user");
            }

            return Ok(list);
        }

        [HttpGet("Album{UserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserAlbums(int UserId)
        {
            var list = await _jsonPlaceholderService.GetUserAlbums(UserId);
            if (list == null || !list.Any())
            {
                return NotFound("could not find any Album of the user");
            }

            return Ok(list);
        }

        [HttpGet("Post{UserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserPosts(int UserId)
        {
            var list = await _jsonPlaceholderService.GetUserPosts(UserId);
            if (list == null || !list.Any())
            {
                return NotFound("could not find any Post of the user");
            }

            return Ok(list);
        }
    }
}
