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
    [Route("api/Symptoms")]
    public class SymptomsController : ControllerBase
    {
        private readonly ISymptomRepository _repository;
        public SymptomsController(ISymptomRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult> LoadAllStudentsWithSymptoms()
        {
            List<SymptomsModel> listOfAllStudentsWithSymptoms = (List<SymptomsModel>) await _repository.FetchAllSymptomsAsync();
            if(listOfAllStudentsWithSymptoms.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(listOfAllStudentsWithSymptoms);
        }
    }
}
