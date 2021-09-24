using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Steps.Commom
{
    [Binding]
    class DatabaseSteps
    {
        private readonly PostgresDatabaseController postgresDatabaseController;

        public DatabaseSteps(PostgresDatabaseController postgresDatabaseController)
        {
            this.postgresDatabaseController = postgresDatabaseController;
        }

        [Given(@"que a base de dados esteja limpa")]
        public async Task DadoQueABaseDeDadosEstejaLimpa()
        {
            await postgresDatabaseController.ClearDatabase();
        }

        [Then(@"o registro estará disponível na tabela '(.*)' da base de dados")]
        public async Task EntaoORegistroEstaraDisponivelNaTabelaDaBaseDeDados(string tableName, Table table)
        {
            var currentItens = await postgresDatabaseController.SelectFrom(tableName, table);

            Assert.NotZero(currentItens.Count(), $"Não foram encontrados registros na tabela {tableName}.");

            var actualJsonResponse = JsonConvert.SerializeObject(currentItens);
            var expectedJsonResponse = JsonConvert.SerializeObject(table.CreateDynamicSet()).Replace("'", "");

            Assert.IsTrue(JToken.DeepEquals(JToken.Parse(actualJsonResponse), JToken.Parse(expectedJsonResponse)), $"Conteúdo atual do retorno \n{actualJsonResponse} diferente do esperado \n{expectedJsonResponse}.");
        }

        [Given(@"os registros sejam inseridos na tabela '(.*)' da base de dados")]
        public async Task DadoOsRegistrosSejamInseridosNaTabelaDaBaseDeDados(string tableName, Table table)
        {
            await postgresDatabaseController.InsertInto(tableName, table);
        }

    }
}
