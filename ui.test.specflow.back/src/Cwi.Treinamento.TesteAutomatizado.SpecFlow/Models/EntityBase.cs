using Newtonsoft.Json;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Models
{
    abstract class EntityBase
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
