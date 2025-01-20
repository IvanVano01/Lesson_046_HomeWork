using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.HomeWork.Develop.CommonServices.Wallet
{
    public class WalletService : IDataReader<PlayerData>, IDataWrite<PlayerData>
    {
        private Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies = new(); // словарь с валютами

        private int _valueToSpend;
        private int _valueToAdd;

        public WalletService(PlayerDataProvider playerDataProvider) 
        {
            playerDataProvider.RegisterWriter(this);// регистрируем себя в провайдере
            playerDataProvider.RegisterReader(this);            
        }

        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList(); // св-во, для просмотра доступных нам валют

        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes type)
            => _currencies[type];                                    // для получение нужной валюты по ключу из словаря валют

        public bool HasEnough(CurrencyTypes type, int amount)
            => _currencies[type].Value >= amount;                   // проверка, хватает ли в кашельке кол-во запрашиваемой валюты

        public void Spend(CurrencyTypes type) // метод получения валюты из кошелька
        {
            if (HasEnough(type, _valueToSpend) == false)
                throw new ArgumentException(type.ToString());

            _currencies[type].Value -= _valueToSpend;
        }

        public void Add(CurrencyTypes type) => _currencies[type].Value += _valueToAdd; // добавляем в кошелёк валюту

        public void ReadFrom(PlayerData data) // здесь считываем данные из "PlayerData" и записываем в "_currencies"
        {
            foreach (KeyValuePair<CurrencyTypes, int> currency in data.WalletData) //из даты данных игрока проходимся по словарю валют и добавляем в кошелёк
            {
                if(_currencies.ContainsKey(currency.Key)) // если такая валюта есть, то добавляем кол-во этой валюты 
                    _currencies[currency.Key].Value = currency.Value;
                else
                    _currencies.Add(currency.Key, new ReactiveVariable<int>(currency.Value)); // если такой валюты нет, то добавляем новую валюту
            }

            _valueToSpend = data.ValueToSpend;
            _valueToAdd = data.ValueToAdd;
        }

        public void WriteTo(PlayerData data) // здесь наоборот считываем данные из "_currencies" и записываем в "PlayerData"
        {
            foreach(KeyValuePair<CurrencyTypes, ReactiveVariable<int>> currency in _currencies)
            {
                if (data.WalletData.ContainsKey(currency.Key))// если уже в самом кошельке есть такой тип валюты
                    data.WalletData[currency.Key] = currency.Value.Value;// то уже из кошелька записываем данные в PlayerData
                else
                    data.WalletData.Add(currency.Key, currency.Value.Value);
            }
        }
    }
}
