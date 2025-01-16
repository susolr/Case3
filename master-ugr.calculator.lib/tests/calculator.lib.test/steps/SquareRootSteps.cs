using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace calculator.lib.test.steps
{
    [Binding]
    public class SquareRootSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public SquareRootSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the number (.*)")]
        public void GivenANumber(string number)
        {
            // Almacena el número en el contexto para usarlo en los pasos posteriores
            _scenarioContext.Add("number", double.Parse(number));
        }

        [When(@"number (.*) is checked for its square root")]
        public void WhenNumberIsCheckedForItsSquareRoot(string number)
        {
            // Obtén el número y calcula su raíz cuadrada
            double parsedNumber = double.Parse(number);
            var squareRoot = NumberAttributter.SquareRoot(parsedNumber);

            // Almacena el resultado en el contexto
            _scenarioContext.Add("squareRoot", squareRoot);
        }

        [Then(@"the answer to the square root is (.*)")]
        public void ThenTheAnswerToTheSquareRootIs(string result)
        {
            // Convierte el resultado esperado
            double expectedSquareRoot = double.Parse(result);

            // Obtiene el resultado calculado
            var squareRoot = _scenarioContext.Get<double>("squareRoot");

            // Validación manual sin NUnit
            if (Math.Abs(squareRoot - expectedSquareRoot) > 0.01)
            {
                throw new Exception($"Test failed: Expected {expectedSquareRoot}, but got {squareRoot}");
            }
        }
    }
}
