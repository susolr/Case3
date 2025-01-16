using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace calculator.frontend.Controllers
{
    public struct Triple
    {
        public string isPrime { get; }
        public string isOdd { get; }
        public string sqrt { get; }

        public Triple(string prime, string odd, string sqrt)
        {
            isPrime = prime;
            isOdd = odd;
            sqrt = sqrt;
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

        private Triple ExecuteOperation(string number)
        {
            bool? raw_prime =  null;
            bool? raw_odd = null;
            double? raw_sqrt = null;
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            var url = $"{base_url}/api/Calculator/number_attribute?number={number}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };
            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body);
                var prime = json["prime"];
                var odd = json["odd"];
                var srqt = json["square_root"];
                if (prime != null)
                {
                    raw_prime = prime.Value<bool>();
                }
                if (odd != null)
                {
                    raw_odd = odd.Value<bool>();
                }
                if(srqt != null){
                    raw_sqrt = srqt.Value<double>();
                }

            }
            var isPrime = "unknown";
            if (raw_prime != null && raw_prime.Value)
            {
                isPrime = "Yes";
            }
            else if (raw_prime != null && !raw_prime.Value)
            {
                isPrime = "No";
            }
            var isOdd = "unknown";
            if (raw_odd != null && raw_odd.Value)
            {
                isOdd = "Yes";
            }
            else if (raw_odd != null && !raw_odd.Value)
            {
                isOdd = "No";
            }
            var sqrtResult = "unknown";
            if(raw_sqrt != null){
                sqrtResult = raw_sqrt.Value.ToString("N0");
            }
            return new Triple(isPrime,isOdd,sqrtResult);
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
