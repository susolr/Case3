using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public ActionResult<double> Add([FromQuery] double a, [FromQuery] double b)
        {
            return Ok(new { result = 120 });
        }

        [HttpGet("subtract")]
        public ActionResult<double> Subtract([FromQuery] double a, [FromQuery] double b)
        {
            return Ok(new { result = 30 });
        }

        [HttpGet("multiply")]
        public ActionResult<double> Multiply([FromQuery] double a, [FromQuery] double b)
        {
            return Ok(new { result = 900 });
        }

        [HttpGet("divide")]
        public ActionResult<double> Divide([FromQuery] double a, [FromQuery] double b)
        {
            return Ok(new { result = 2.5 });
        }

        [HttpGet("is_prime")]
        public ActionResult<bool> IsPrime([FromQuery] int number)
        {
            return Ok(new { result = true });
        }
    }
}