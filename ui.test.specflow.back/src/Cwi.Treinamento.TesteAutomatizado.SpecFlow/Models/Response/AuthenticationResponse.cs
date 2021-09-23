using Newtonsoft.Json;
using System;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models.Response
{
    class AuthenticationResponse
    {
        public DateTime Created { get; private set; }

        public DateTime Expiration { get; private set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; private set; }
    }
}
