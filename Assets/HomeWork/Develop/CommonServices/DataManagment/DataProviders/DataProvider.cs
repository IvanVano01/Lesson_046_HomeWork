using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public abstract class DataProvider<TData> where TData : ISaveData // базовый класс данных,
                                                                      // работать может только с классами унаследованными от "ISaveData"
    {
        private readonly ISaveLoadService _saveLoadService;// сервис загрузки, создан

        private List<IDataWrite<TData>> _writers = new();// список сервисов которым надо записать данные(сохранить)
        private List<IDataReader<TData>> _readers = new();//список сервисов которым надо считать данные

        public DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private TData Data { get; set; }//  поле для класса данных,с которыми будем работать

        public void RegisterWriter(IDataWrite<TData> writer)
        {
            if (_writers.Contains(writer))// если список уже содержит сервис который нужно внести
                throw new ArgumentException(nameof(writer));

            _writers.Add(writer);
        }

        public void RegisterReader(IDataReader<TData> reader)
        {
            if(_readers.Contains(reader))// если список уже содержит сервис который нужно внести
                throw new ArgumentException(nameof(reader));

            _readers.Add(reader);
        }

        public void Load()
        {
            if (_saveLoadService.TryLoad(out TData data))// пытаемся подгрузить данные и если всё ок, то грузим
                Data = data;
            else
                Reset();// если данные не получилось подгрузить, то ресетим их

            foreach(IDataReader<TData> reader in _readers)// проходимся по списку сервисов, которым нужно считать данные            
                reader.ReadFrom(Data);// и вызываем метод считывания данных            
        }

        protected abstract TData GetOriginData();// абстрактный метод, у каждого класса с данными будет своя реализация   

        public void Save()
        {
            foreach(IDataWrite<TData> writer in _writers)// проходимся по списку сервисов, которым нужно записать данные 
                writer.WriteTo(Data);

            _saveLoadService.Save(Data);
        }

        private void Reset()// метод ресета данных, создание дефолтных
        {
            Data = GetOriginData();
            Save();
        }
    }
}
