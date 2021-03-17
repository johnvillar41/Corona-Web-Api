using Microsoft.AspNetCore.Mvc;
using SoftEng2BackendAPI.ApikeyAttribute;
using SoftEng2BackendAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Controllers
{
    [ApiController]
    [ApikeyAuth]
    [Route("api/MclTotal")]
    public class MclTotalController : ControllerBase
    {
        private IMclTotalRepository _repository;
        public MclTotalController(IMclTotalRepository repository)
        {
            _repository = repository;
        }
        //GET api/MclTotal
        [HttpGet]
        public async Task<ActionResult> LoadTotals()
        {
            var mclTotal = await _repository.FetchTotals();
            if (mclTotal == null)
            {
                return NotFound("No Results");
            }
            return Ok(mclTotal);
        }
    }
}
