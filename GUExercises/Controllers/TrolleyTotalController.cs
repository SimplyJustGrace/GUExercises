using GUExercises.Models;
using GUExercises.Services;
using Microsoft.AspNetCore.Mvc;

namespace GUExercises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        TrolleyTotalService _service;

        public TrolleyTotalController(TrolleyTotalService service)
        {
            _service = service;
        }

        // POST api/trolleytotal
        [HttpPost("trolleytotal")]
        public decimal GetTrolleyTotal([FromBody] TrolleyModel trolley)
        {
            return _service.GetTrolleyTotal(trolley);
        }
    }
}