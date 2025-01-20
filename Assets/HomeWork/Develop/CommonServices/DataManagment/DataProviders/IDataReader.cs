namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData // интерфейс будут реализовывать те сервисы,которым нужно считать данные при загрузки
    {
        void ReadFrom(TData data);
    }
}
