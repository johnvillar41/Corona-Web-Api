using Microsoft.AspNetCore.Mvc;
using SoftEng2BackendAPI.ApikeyAttribute;
using SoftEng2BackendAPI.Models;
using SoftEng2BackendAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Controllers
{
    [ApiController]
    [ApikeyAuth]
    [Route("api/Exposure")]
    public class ExposureController : ControllerBase
    {
        private readonly IExposureRepository _repository;
        public ExposureController(IExposureRepository repository)
        {
            _repository = repository;
        }
        //GET api/Exposure
        [HttpGet]
        public async Task<ActionResult> LoadAllExposedStudents()
        {
            List<ExposureModel> exposureList = (List<ExposureModel>)await _repository.FetchAllExposedStudentsAsync();
            if (exposureList.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(exposureList);
        }
        //GET api/Exposure/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> LoadExposedStudentGivenAnID(int id)
        {
            List<UserModel> exposedStudents = (List<UserModel>)await _repository.FetchExposedStudentsGivenByIDAsync(id);
            if(exposedStudents.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(exposedStudents);
        }
        //POST api/Exposure
        [HttpPost]
        public async Task<ActionResult> EnterNewExposedStudent([FromBody]ExposureModel exposedModel)
        {           
            await _repository.InsertNewExposedStudentAsync(exposedModel);
            return Ok();
        }
    }
}
