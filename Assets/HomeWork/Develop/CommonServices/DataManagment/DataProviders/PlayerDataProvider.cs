using Assets.HomeWork.Develop.CommonServices.ConfigsManagment;
using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>// конкретный провайдер для класса игрока
    {
        // тут будем передавать сервис конфигов
        private ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(ISaveLoadService saveLoadService,
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()// данные по умолчанию, создаются если ещё не разу не сохраняли
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(), // создаём данные для игрока(кошелёк с валютами)
                CompletedLevels = new(),

                ValueToSpend = _configsProviderService.StartWalletConfig.ValueToSpend,
                ValueToAdd = _configsProviderService.StartWalletConfig.ValueToAdd
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach(CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes))) // заполняем словарь валютами из энама"CurrencyType"
            {
                walletData.Add(currencyType, _configsProviderService.StartWalletConfig.GetSartValueFor(currencyType));//подгружаем значение валют из конфига
            }

            return walletData;
        }
    }
}
