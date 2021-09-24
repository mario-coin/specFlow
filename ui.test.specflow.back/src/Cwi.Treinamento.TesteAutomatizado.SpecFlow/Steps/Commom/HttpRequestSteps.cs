using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers;
using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Steps.Commom
{
    [Binding]
    class HttpRequestSteps
    {
        private readonly HttpRequestController httpRequestController;
        public HttpRequestSteps(HttpRequestController httpRequestController)
        {
            this.httpRequestController = httpRequestController;
        }

        [Given(@"seja feita uma chamada do tipo '(.*)' para o endpoint '(.*)'")]
        public async Task DadoSejaFeitaUmaChamadaDoTipoParaOEndpoint(string httpMethodName, string endpoint)
        {
            await httpRequestController.SendAsync(endpoint, httpMethodName);
        }

        [Given(@"seja feita uma chamada do tipo '(.*)' para o endpoint '(.*)' com o corpo da requisição")]
        public async Task DadoSejaFeitaUmaChamadaDoTipoParaOEndpointComOCorpoDaRequisicao(string httpMethodName, string endpoint, string body)
        {
            httpRequestController.AddJsonBody(body);

            await httpRequestController.SendAsync(endpoint, httpMethodName);
        }

        [Then(@"o código de retorno será '(.*)'")]
        public void EntaoOCodigoDeRetornoSera(int httpStatusCode)
        {
            Assert.AreEqual(httpStatusCode, (int)httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"a chamada do tipo '(.*)' para o endpoint '(.*)' terá o seguinte corpo")]
        public async Task EntaoAChamadaDoTipoParaOEndpointTeraOSeguinteCorpo(string httpMethodName, string endpoint, string expectedJsonResponse)
        {
            await httpRequestController.SendAsync(endpoint, httpMethodName);

            var actualResponse = await httpRequestController.GetTypedResponseBody<IEnumerable<Employee>>();
            var expectedResponse = JsonConvert.DeserializeObject<IEnumerable<Employee>>(expectedJsonResponse);

            Assert.AreEqual(actualResponse.Count(), expectedResponse.Count(), $"A quantidade de ítens entre a lista de ítens obtida e a esperada são diferentes.");

            var actualJsonResponse = JsonConvert.SerializeObject(actualResponse);

            Assert.IsTrue(JToken.DeepEquals(JToken.Parse(actualJsonResponse), JToken.Parse(expectedJsonResponse)), $"Conteúdo atual do retorno \n{actualJsonResponse} diferente do esperado \n{expectedJsonResponse}.");

        }

    }
}
