using Newtonsoft.Json;

namespace ConsimpleApi.Model
{
    class Category
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
