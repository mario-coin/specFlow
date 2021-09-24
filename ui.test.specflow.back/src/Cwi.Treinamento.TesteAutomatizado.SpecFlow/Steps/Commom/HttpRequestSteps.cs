using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers;
using NUnit.Framework;
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

    }
}
