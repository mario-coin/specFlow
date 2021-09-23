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

        [Given(@"que seja solicitado a cria��o de um novo funcion�rio")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionario()
        {
            httpRequestController.AddJsonBody(new EmplyeeCreateRequest
            {
                Name = "Funcion�rio 1",
                Email = "funcionario1@empresa.com"
            });

            await httpRequestController.SendAsync("v1/employees", "POST");
        }

        [Given(@"que seja solicitado a cria��o de um novo funcion�rio sem o preenchimento dos campos obrigat�rios")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionarioSemOPreenchimentoDosCamposObrigatorios()
        {
            httpRequestController.AddJsonBody(new { });

            await httpRequestController.SendAsync("v1/employees", "POST");
        }

        [Then(@"o funcion�rio n�o ser� cadastrado")]
        public void EntaoOFuncionarioNaoSeraCadastrado()
        {
            Assert.AreNotEqual(HttpStatusCode.Created, httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"que o funcion�rio seja cadastrado")]
        public void EntaoQueOFuncionarioSejaCadastrado()
        {
            Assert.AreEqual(HttpStatusCode.Created, httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"ser� retornado uma mensagem de erro de autentica��o")]
        public void EntaoSeraRetornadoUmaMensagemDeErroDeAutenticacao()
        {
            Assert.AreEqual(HttpStatusCode.Unauthorized, httpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"ser� retornado uma mensagem de falha de preenchimento de campos obrigat�rios")]
        public void EntaoSeraRetornadoUmaMensagemDeFalhaDePreenchimentoDeCamposObrigatorios()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, httpRequestController.GetResponseHttpStatusCode());
        }

    }
}