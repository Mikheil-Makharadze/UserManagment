using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManagment.API.DTO.UserProfileDTO;
using UserManagment.Core.Entities;
using UserManagment.Core.Interfaces;
using UserManagment.infrastructure.Services;

namespace UserManagment.API.Controllers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserProfileController : BaseAPIController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;

        public UserProfileController(UserManager<User> userManager, IUserProfileService userProfileService, IMapper mapper)
        {
            _userManager = userManager;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileDetailDTO>> GetUserProfile(int id)
        {
            var userProfile = await _userProfileService.GetByIdAsync(id, n => n.User);
            if (userProfile == null)
            {
                return NotFound("User profile does not exist");
            }
            var userProfiledetails = _mapper.Map<UserProfileDetailDTO>(userProfile);

            return Ok(userProfiledetails);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserProfileDTO>>> GetUserProfiles()
        {
            var userProfiles = await _userProfileService.GetAllAsync();

            var userProfilesDTO = _mapper.Map<List<UserProfileDTO>>(userProfiles);

            return Ok(userProfilesDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserProfileDTO>> CreateUserProfile(UserProfileCreateDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var existingProfile = await _userProfileService.GetUserProfileByUserId(user.Id);
            if (existingProfile != null)
            {
                return BadRequest("User profile already exists.");
            }

            var userProfile = _mapper.Map<UserProfile>(model);

            userProfile.UserId = user.Id;

            var createdUserProfile = await _userProfileService.AddAsync(userProfile);

            var userProfileDTO = _mapper.Map<UserProfileDTO>(createdUserProfile);

            return CreatedAtAction(nameof(GetUserProfile), new { id = userProfileDTO.Id }, userProfileDTO);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileDTO>> UpdateUserProfile(int id, UserProfileUpdateDTO model)
        {
            var existingProfile = await _userProfileService.GetByIdAsync(id);
            if (existingProfile == null)
            {
                return NotFound("User profile not found.");
            }

            _mapper.Map(model, existingProfile);

            var updatedUserProfile = await _userProfileService.UpdateAsync(existingProfile);

            var userProfileDTO = _mapper.Map<UserProfileDTO>(updatedUserProfile);

            return Ok(userProfileDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var userProfile = await _userProfileService.GetByIdAsync(id);
            if (userProfile == null)
            {
                return NotFound("User profile not found.");
            }

            await _userProfileService.DeleteAsync(userProfile);

            return Ok("User profile deleted successfully.");
        }
    }
}
