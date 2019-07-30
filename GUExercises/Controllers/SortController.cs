using GUExercises.Models;
using GUExercises.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUExercises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        SortService _service;

        public SortController(SortService service)
        {
            _service = service;
        }

        // GET api/sort
        [HttpGet("sort")]
        public async Task<ActionResult<List<ProductModel>>> SortProducts(string sortOption)
        {
            return await _service.Sort(sortOption);
        }
    }
}
