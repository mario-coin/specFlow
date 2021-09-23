using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers;
using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models.Request;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Steps.Employee
{
    [Binding]
    [Scope(Tag = "CreateEmployee")]
    public class AuthenticationSteps
    {
        private readonly HttpRequestController httpRequestController;
        private readonly IConfiguration configuration;

        public AuthenticationSteps(HttpRequestController httpRequestController, IConfiguration configuration)
        {
            this.httpRequestController = httpRequestController;
            this.configuration = configuration;
        }

        [Given(@"que seja solicitado a criação de um novo funcionário")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionario()
        {
            httpRequestController.AddJsonBody(new EmplyeeCreateRequest
            {
                Name = "Funcionário 1",
                Email = "funcionario1@empresa.com"
            });

            await httpRequestController.SendAsync("v1/employees", "POST");
        }

        [Given(@"que seja solicitado a criação de um novo funcionário sem o preenchimento dos campos obrigatórios")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionarioSemOPreenchimentoDosCamposObrigatorios()
        {
            httpRequestController.AddJsonBody(new { });

            await httpRequestController.SendAsync("v1/employees", "POST");
        }

        [Then(@"o funcionário não será cadastrado")]
        public void EntaoOFuncionarioNaoSeraCadastrado()
        {
            Assert.AreNotEqual(HttpStatusCode.Created, httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"que o funcionário seja cadastrado")]
        public void EntaoQueOFuncionarioSejaCadastrado()
        {
            Assert.AreEqual(HttpStatusCode.Created, httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"será retornado uma mensagem de erro de autenticação")]
        public void EntaoSeraRetornadoUmaMensagemDeErroDeAutenticacao()
        {
            Assert.AreEqual(HttpStatusCode.Unauthorized, httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"será retornado uma mensagem de falha de preenchimento de campos obrigatórios")]
        public void EntaoSeraRetornadoUmaMensagemDeFalhaDePreenchimentoDeCamposObrigatorios()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, httpRequestController.GetResponseHttpStatusCode());
        }

    }
}