using System;
using GUExercises.Models;
using GUExercises.Services;
using Microsoft.AspNetCore.Mvc;

namespace GUExercises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        AnswersService _service;

        public AnswersController(AnswersService service)
        {
            _service = service;
        }

        // GET api/user
        [HttpGet("user")]
        public ActionResult<UserModel> GetUser(string user)
        {
            return _service.GetUser();
        }
    }
}