using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace calculator.frontend.Controllers
{
    public struct Triple
    {
        public string isPrime { get; }
        public string isOdd { get; }
        public string sqrt { get; }

        public Triple(string prime, string odd, string iSqrt)
        {
            isPrime = prime;
            isOdd = odd;
            sqrt = iSqrt;
        }
    }
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
            Console.WriteLine("Antes de Execute");
            var result = ExecuteOperation(number);
            Console.WriteLine("Despues de execute");
            ViewBag.IsPrime = result.isPrime;
            ViewBag.IsOdd   = result.isOdd;
            ViewBag.Sqrt    = result.sqrt;
            Console.WriteLine(ViewBag.Sqrt);
            return View();
        }

        private Triple ExecuteOperation(string number)
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

        private Triple ParseAttributes(JObject attributes)
        {
            bool? rawPrime = attributes["prime"]?.Value<bool>();
            bool? rawOdd = attributes["odd"]?.Value<bool>();
            double? raw_sqrt = attributes["square_root"]?.Value<double>();

            string isPrime = rawPrime == true ? "Yes" : rawPrime == false ? "No" : "unknown";
            string isOdd = rawOdd == true ? "Yes" : rawOdd == false ? "No" : "unknown";
            string sqrtResult = raw_sqrt != null ? raw_sqrt.Value.ToString("N2") : "unknown";

            return new Triple(isPrime,isOdd,sqrtResult);
        }
    }
}
