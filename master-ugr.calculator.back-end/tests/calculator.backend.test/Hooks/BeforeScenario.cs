using TechTalk.SpecFlow;
using static System.Net.WebRequestMethods;

namespace calculator.backend.test.Hooks
{
    [Binding]
    public class BeforeScenario
    {
        private readonly ScenarioContext _scenarioContext;
        public BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void ScenarioPreparation(ScenarioContext scenarioContext)
        {
            var urlBase = GetUrlBase();
            Console.WriteLine($"Using URL: {urlBase}");
            _scenarioContext.Add("urlBase", urlBase);
        }

        private string GetUrlBase()
        {
            var urlBase = Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL")
                          ?? Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL_UAT")
                          ?? "https://localhost:7012/";

            return urlBase.EndsWith("/") ? urlBase : urlBase + "/";
        }

    }
}
