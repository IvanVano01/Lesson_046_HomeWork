using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonUI.Wallet
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        // модель
        private WalletService _walletService;
        private WalletPresenterFactory _factory;

        private List<CurrencyPresenter> _currencyPresenters = new();

        //вью
        private IconWithTextListView _view;


        public WalletPresenter(WalletService walletService, IconWithTextListView view, WalletPresenterFactory factory)
        {
            _walletService = walletService;
            _view = view;
           _factory = factory;
        }

        public void Inintialize()
        {
            foreach(CurrencyTypes currencyTypes in _walletService.AvailableCurrencies)
            {
                // создать вью
                IconWithText currencyView = _view.SpawnElement();

                //создать презентер
                CurrencyPresenter currencyPresenter = _factory.CreateCurrencyPresenter(currencyView, currencyTypes);

                // проинитить презентер
                currencyPresenter.Inintialize();
                _currencyPresenters.Add(currencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach(CurrencyPresenter currencyPresenter in _currencyPresenters)
            {
                _view.Remove(currencyPresenter.View);
                currencyPresenter.Dispose();
            }

            _currencyPresenters.Clear();
        }
    }
}
