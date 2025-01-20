using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.Wallet;
using Assets.HomeWork.Develop.Configs.Common.Wallet;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;

namespace Assets.HomeWork.Develop.CommonUI.Wallet
{
    public class CurrencyPresenter : IInitializable, IDisposable// презеттер для связки view и model
    {
        //бизнес логика
        private IReadOnlyVariable<int> _currency;// ссылка на модель
        private CurrencyTypes _currencyType;

        //визуал
        private IconWithText _view;// (вьюха)
        private CurrencyIconsConfig _currencyIconsConfig;

        public CurrencyPresenter(IReadOnlyVariable<int> currency, 
            CurrencyTypes currencyType, 
            IconWithText currencyView, 
            CurrencyIconsConfig currencyIconsConfig)
        {
            _currency = currency;
            _currencyType = currencyType;
            _view = currencyView;
            _currencyIconsConfig = currencyIconsConfig;
        }

        public IconWithText View => _view;

        public void Inintialize()
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));            

            _currency.Changed += OnCurrencyChanged;
        }

        public void Dispose()
        {
            _currency.Changed -= OnCurrencyChanged;
        }

        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);        

        private void UpdateValue(int value) => _view.SetText(value.ToString());       
    }
}
