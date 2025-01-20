namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer; // поле для сериализатора данных
        private readonly IDataRepository _repository; // поле для репозитория, хранилища данных

        public SaveLoadService(IDataSerializer serializer, IDataRepository repository)
        {
            _serializer = serializer;
            _repository = repository;
        }

        public void Save<TData>(TData data) where TData : ISaveData // метод сохранения данных
        {
            string serializeData = _serializer.Serialize(data);// принимаем "TData data" данные и сериализуем ввиде строки

            _repository.Write(SaveDataKeys.GetKeyFor<TData>(), serializeData);// записываем сереализованные данные "serializeData" в репозиторий(хранилище),
                                                                              // предварительно получив ключ по типу данных "TData"
        }

        public bool TryLoad<TData>(out TData data) where TData : ISaveData // метод подгрузки данных 
        {
            string key = SaveDataKeys.GetKeyFor<TData>(); // получаю ключ по данных которые хочу загрузить

            if(_repository.Exists(key) == false) // если в хранилище, нет данных по такому ключу
            {
                data = default(TData);// возвращаем дефолтные значения для запрашиваемого типа данных( для int =0 это будет ноль и т.д)
                return false;
            }

            string serializedData =_repository.Read(key);// в хранилищи есть данные по ключу, записываем их в переменную, сеарелизованные данные

            data = _serializer.Deserialize<TData>(serializedData);// записываем в переменную, десеарелизованные данные,
                                                                  // т.е приводим их к нужному типу
            return true;
        }
    }
}
