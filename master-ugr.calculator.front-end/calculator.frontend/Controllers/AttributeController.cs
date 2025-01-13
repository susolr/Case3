using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace calculator.frontend.Controllers
{
    public class AttributeController : Controller
    {
        private string base_url =
            Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ??
            "https://susolr-calculator-backend-uat.azurewebsites.net";

        const string api = "api/Calculator";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string number)
        {
            var result = ExecuteOperation(number);

            ViewBag.IsPrime = result.Key;
            ViewBag.IsOdd = result.Value;

            return View();
        }

        private KeyValuePair<string, string> ExecuteOperation(string number)
        {
            string url = BuildUrl(number);

            var attributes = FetchAttributesFromApi(url);

            return ParseAttributes(attributes);
        }

        private string BuildUrl(string number)
        {
            return $"{base_url}/api/Calculator/number_attribute?number={number}";
        }

        private JObject FetchAttributesFromApi(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode();

                var body = response.Content.ReadAsStringAsync().Result;
                return JObject.Parse(body);
            }
        }

        private KeyValuePair<string, string> ParseAttributes(JObject attributes)
        {
            bool? rawPrime = attributes["prime"]?.Value<bool>();
            bool? rawOdd = attributes["odd"]?.Value<bool>();

            string isPrime = rawPrime == true ? "Yes" : rawPrime == false ? "No" : "unknown";
            string isOdd = rawOdd == true ? "Yes" : rawOdd == false ? "No" : "unknown";

            return new KeyValuePair<string, string>(isPrime, isOdd);
        }
    }
}
