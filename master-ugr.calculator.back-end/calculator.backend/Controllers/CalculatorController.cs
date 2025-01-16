using calculator.lib;
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
            var final_result = Calculator.Add((int)a, (int)b);
            return Ok(new { result = final_result });
        }

        [HttpGet("subtract")]
        public ActionResult<double> Subtract([FromQuery] double a, [FromQuery] double b)
        {
            var final_result = Calculator.Subtract((int)a, (int)b);
            return Ok(new { result = final_result });
        }

        [HttpGet("multiply")]
        public ActionResult<double> Multiply([FromQuery] double a, [FromQuery] double b)
        {
            var final_result = Calculator.Multiply((int)a, (int)b);
            return Ok(new { result = final_result });
        }

        [HttpGet("divide")]
        public ActionResult<double> Divide([FromQuery] double a, [FromQuery] double b)
        {
            var final_result = Calculator.Divide((int)a, (int)b);

            return Ok(new { result = final_result });
        }

        [HttpGet("is_prime")]
        public ActionResult<bool> IsPrime([FromQuery] int number)
        {
            var is_prime = NumberAttributter.IsPrime(number);
            return Ok(new { result = is_prime });
        }

        [HttpGet("square_root")]
        public ActionResult<bool> SquareRoot([FromQuery] int number)
        {
            var sqrt = NumberAttributter.SquareRoot(number);
            return Ok(new { result = sqrt });
        }

        [HttpGet("number_attribute")]
        public ActionResult<bool> NumberAttribute([FromQuery] int number)
        {
            var is_prime = NumberAttributter.IsPrime(number);
            var is_odd = NumberAttributter.IsOdd(number);
            var sqrt = NumberAttributter.SquareRoot(number);
            return Ok(new { odd = is_odd, prime = is_prime, square_root = sqrt });
        }
    }
}