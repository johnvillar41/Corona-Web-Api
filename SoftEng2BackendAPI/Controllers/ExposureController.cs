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
            List<ExposureModel> exposureList = (List<ExposureModel>)await _repository.FetchAllExposedStudents();
            if (exposureList.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(exposureList);
        }
    }
}
