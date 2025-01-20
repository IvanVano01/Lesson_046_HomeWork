namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);// сереализация данных(метод возвращает строку данных)

        TData Deserialize<TData>(string serializedData);// десериализация данных( из строки в другой тип данных для сохранения)
    }
}
