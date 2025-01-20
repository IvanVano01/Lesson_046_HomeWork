using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    [Serializable]

    public class PlayerData : ISaveData // данные игрока, которые будем сохранять
    {
        public Dictionary<CurrencyTypes, int> WalletData;// в качестве данных будет словарь с валютами

        public List<int> CompletedLevels;

        public int ValueToSpend;
        public int ValueToAdd;
    }
}
