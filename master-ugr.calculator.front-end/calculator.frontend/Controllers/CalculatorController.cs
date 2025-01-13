using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace calculator.frontend.Controllers
{
    public class CalculatorController : Controller
    {
        // the base_url is obtained from environment variable
        // CALCULATOR_BACKEND_URL. If it is not present, it uses
        // "https://susolr-calculator-backend-uat.azurewebsites.net";
        private string base_url =
            Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ??
            "https://susolr-calculator-backend-uat.azurewebsites.net";

        const string api = "api/Calculator";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string firstNumber, string secondNumber, string operation)
        {
            double num1 = Convert.ToDouble(firstNumber);
            double num2 = Convert.ToDouble(secondNumber);

            ViewBag.Result = ExecuteOperation(operation.ToLower(), num1, num2);
            return View();
        }

        private double ExecuteOperation(string operation, double num1, double num2)
        {
            string url = BuildUrl(operation, num1, num2);

            return FetchResultFromApi(url);
        }

        private string BuildUrl(string operation, double num1, double num2)
        {
            return $"{base_url}/api/Calculator/{operation}?a={num1}&b={num2}";
        }

        private double FetchResultFromApi(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                var body = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body);

                return json["result"]?.Value<double>() ?? 0.0;
            }
        }
    }
}
