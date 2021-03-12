using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftEng2BackendAPI.ApikeyAttribute;
using SoftEng2BackendAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Controllers
{
    [ApiController]
    [ApikeyAuth]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///     UserController will have no delete route you can only update its status
        /// </summary>
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        //GET api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> LoadAllUsers()
        {
            var userList = await _repository.FetchUsersAsync();
            return Ok(userList);
        }
        //GET api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> FetchSpecificUser(int id)
        {
            var specificUser = await _repository.FetchSpecificUserAsync(id);
            if (specificUser != null)
            {
                return Ok(specificUser);
            }
            return NotFound();
        }
        //GET api/User/{user}/{password}
        [HttpGet("{user}/{pass}")]
        public async Task<ActionResult> LoadLoginUser(string user, string pass)
        {
            var checkLogin = await _repository.LoginUserAsync(user, pass);
            if (checkLogin)
            {
                return Ok("Congrats");
            }
            return NotFound();
        }
        //POST api/User
        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] UserModel newUser)
        {
            if (newUser == null)
            {
                return NoContent();
            }
            await _repository.RegisterNewUserAsync(newUser);
            return Ok(newUser);
        }
        //PUT api/User/{id}/{status}
        [HttpPut("{id}/{status}")]
        public async Task<ActionResult> UpdateStatusOfUser(int id, string status)
        {
            if (status == null && String.IsNullOrEmpty(id.ToString()))
            {
                return NoContent();
            }
            await _repository.UpdateStatusOfUserAsync(id, status);
            return Ok();
        }
        
    }
}
