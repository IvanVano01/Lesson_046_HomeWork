using Newtonsoft.Json;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public class JsonSerializer : IDataSerializer // сериализатор и десириализатор данных в Json
    {
        public TData Deserialize<TData>(string serializedData)//десериализуем, передаём строку и получаем объект(класс)
        {
           return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings
           {
               TypeNameHandling = TypeNameHandling.Auto
           });
        }

        public string Serialize<TData>(TData data)// сереализуем, передаём объект, получаем строку
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }
}
