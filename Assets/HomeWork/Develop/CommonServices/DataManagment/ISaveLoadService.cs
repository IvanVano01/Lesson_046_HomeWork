namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public interface ISaveLoadService// интерфейс для сервиса сохранения данных
    {
        bool TryLoad<TData>(out TData data) where TData : ISaveData; // загрузить данные, если они есть

        void Save<TData>(TData data) where TData : ISaveData; // сохранить данные
    }
}
