using Newtonsoft.Json;

namespace TestLib.Abstractions
{
    public class JsonSerializer : ISerializer
    {
        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };

        public T? Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj, jsonSettings);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, jsonSettings);
        }
    }
}
