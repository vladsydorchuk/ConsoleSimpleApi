using Newtonsoft.Json;

namespace ConsimpleApi.Model
{
    partial class Product
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }
    }
}
