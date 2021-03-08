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
    [Route("[api/controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public Task<IEnumerable<UserModel>> Get()
        {
            return null;
        }
    }
}
