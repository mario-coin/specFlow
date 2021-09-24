using Newtonsoft.Json;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models
{
    class Employee : EntityBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
