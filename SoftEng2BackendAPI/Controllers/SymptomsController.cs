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
        //GET api/Symptoms
        [HttpGet]
        public async Task<ActionResult> LoadAllStudentsWithSymptoms()
        {
            List<SymptomsModel> listOfAllStudentsWithSymptoms = (List<SymptomsModel>) await _repository.FetchAllPeopleWithSymptomsAsync();
            if(listOfAllStudentsWithSymptoms.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(listOfAllStudentsWithSymptoms);
        }
        //GET api/Symptoms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> LoadSymptomsForStudent(int id)
        {
            List<SymptomsModel> listOfSymptomsForStudent = (List<SymptomsModel>)await _repository.FetchSymptomsForSpecificStudentAsync(id);
            if(listOfSymptomsForStudent.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(listOfSymptomsForStudent);
        }
        //GET api/Symptoms/StudentsWithSymptoms/{symptom}
        [HttpGet("StudentsWithSymptoms/{symptom}")]
        public async Task<ActionResult> LoadStudentsWithSpecificSymptoms(string symptom)
        {
            List<UserModel> listOfStudentsWithSpecificSymptoms = (List<UserModel>)await _repository.FetchStudentsWithSpecificSymptomAsync(symptom);
            if(listOfStudentsWithSpecificSymptoms.Count == 0)
            {
                return NotFound("No Records");
            }
            return Ok(listOfStudentsWithSpecificSymptoms);
        }
        //DELETE api/Symptoms/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAllStudentsSymptoms(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return NotFound();
            }
            await _repository.DeleteAllStudentSymptomsAsync(id);
            return Ok();
        }
        //POST api/Symptoms
        [HttpPost]
        public async Task<ActionResult>InsertNewSymptom([FromBody]SymptomsModel symptoms)
        {
            if(symptoms == null)
            {
                return NotFound();
            }
            await _repository.InsertNewSymptoms(symptoms);
            return Ok();
        }
    }
}
