namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public interface IDataWrite<TData> where TData : ISaveData // интерфейс будут реализовывать те сервисы,котопым нужно записать данные
    {     
        void WriteTo(TData data);
    }
}
