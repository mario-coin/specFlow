using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers;
using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models.Request;
using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models.Response;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Steps.Commom
{
    [Binding]
    public class AuthenticationSteps
    {
        private readonly HttpRequestController httpRequestController;
        private readonly IConfiguration configuration;

        public AuthenticationSteps(HttpRequestController httpRequestController, IConfiguration configuration)
        {
            this.httpRequestController = httpRequestController;
            this.configuration = configuration;
        }

        [Given(@"que o usuário não esteja autenticado")]
        public void DadoQueOUsuarioNaoEstejaAutenticado()
        {
            httpRequestController.RemoveHeader("Authorization");
        }

        [Given(@"que o usuário esteja autenticado")]
        public async Task DadoQueOUsuarioEstejaAutenticado()
        {
            httpRequestController.AddJsonBody(new AuthenticationRequest
            {
                Username = configuration["Authentication:Username"],
                Password = configuration["Authentication:Password"]
            });

            await httpRequestController.SendAsync("v1/public/auth", "POST");

            Assert.AreEqual(HttpStatusCode.OK, httpRequestController.GetResponseHttpStatusCode());

            var authenticationResponse = await httpRequestController.GetTypedResponseBody<AuthenticationResponse>();

            httpRequestController.AddHeader("Authorization", $"Bearer {authenticationResponse.AccessToken}");
        }
    }
}