using BoDi;
using Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.IO;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow
{
    [Binding]
    public class Startup
    {
        private readonly IObjectContainer objectContainer;

        public Startup(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void DependencyInjection()
        {
            var configuration = GetConfiguration();
            var httpRequestController = new HttpRequestController(GetHttpClientFactory(), configuration["UrlBase"]);
            var potgresDatabaseController = new PostgresDatabaseController(new NpgsqlConnection(configuration["ConnectionStrings:NpgsqlConnectionString"]));

            objectContainer.RegisterInstanceAs<IConfiguration>(configuration);
            objectContainer.RegisterInstanceAs<HttpRequestController>(httpRequestController);
            objectContainer.RegisterInstanceAs<PostgresDatabaseController>(potgresDatabaseController);
        }

        private IConfiguration GetConfiguration()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();
        }

        private IHttpClientFactory GetHttpClientFactory()
        {
            return new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>();
        }
    }
}
