using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.HomeWork.Develop.Configs.Common.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]

    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values; // список хранит кол-во конкретной валюты

        [field: SerializeField, Range(100, 300)] public int ValueToSpend { get; private set; }
        [field: SerializeField, Range(100, 300)] public int ValueToAdd { get; private set; }

        private void OnValidate()
        {
            // можно проверить все ли элементы энама представленны в конфиге и т.д
            // нет ли дубликатов
        }

        public int GetSartValueFor(CurrencyTypes currencyType) => _values.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; } // конфиг, типа валюты
            [field: SerializeField] public int Value { get; private set; }          // значение(кол-во) валюты
            
        }
    }
}
