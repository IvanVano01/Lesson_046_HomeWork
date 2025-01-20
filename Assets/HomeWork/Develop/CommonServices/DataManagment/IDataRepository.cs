namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public interface IDataRepository // хранилище данных
    {
        string Read(string key);// считать данные по ключу

        void Write(string key, string serializedData);// записать данные по ключу\

        void Remove(string key);// удалить данные по ключу

        bool Exists(string key);// проверить, есть ли данные по запрашиваему ключу
    }
}
