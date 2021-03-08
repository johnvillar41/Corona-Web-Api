using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftEng2BackendAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        //GET api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> LoadAllUsers()
        {
            return Ok(_repository.FetchUsers());
        }


        //GET api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> FetchSpecificUser(int id)
        {
            var specificUser = await _repository.FetchSpecificUser(id);
            if (specificUser != null)
            {
                return Ok(specificUser);
            }
            return NotFound();
        }

        //GET api/User/{user}/{password}
        [HttpGet("{user}/{pass}")]
        public ActionResult LoadLoginUser(string user, string pass)
        {
            var checkLogin = _repository.LoginUser(user, pass);
            if (checkLogin)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
