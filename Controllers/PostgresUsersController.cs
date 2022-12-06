using InyeccionSQLProyect.Services.Postgres;
using Microsoft.AspNetCore.Mvc;

namespace InyeccionSQLProyect.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class PostgresUsersController : ControllerBase
    {
        private readonly IPUsersServices _userService;

        public PostgresUsersController(IPUsersServices userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }
        
        [HttpGet("obtenerConSeguridad/{name}")]
        public IActionResult GetUserByNameWithSecurity(string name)
        {
            return Ok(_userService.GetUserByNameWithSecurity(name));
        }
        
        [HttpGet("obtenerSinSeguridad/{name}")]
        public IActionResult GetUserByNameWithoutSecurity(string name)
        {
            return Ok(_userService.GetUserByNameWithoutSecurity(name));
        }
        
        [HttpPost("insertarConSeguridad/{name}")]
        public IActionResult InsertUserWithSecurity(string name)
        {
            string result = _userService.InsertUserWithSecurity(name);

            return result.ToUpper() == "ok".ToUpper() ?
            Ok($"Inserción exitosa!") : BadRequest($"ERROR: {result}");
        }
        
        [HttpPost("insertarSinSeguridad/{name}")]
        public IActionResult InsertUserWithoutSecurity(string name)
        {
            string result = _userService.InsertUserWithoutSecurity(name);

            return result.ToUpper() == "ok".ToUpper() ?
            Ok($"Inserción exitosa!") : BadRequest($"ERROR: {result}");
        }
    }
}
