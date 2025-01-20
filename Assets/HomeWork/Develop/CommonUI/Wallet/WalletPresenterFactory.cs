using Assets.HomeWork.Develop.CommonServices.ConfigsManagment;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.Wallet;

namespace Assets.HomeWork.Develop.CommonUI.Wallet
{
    public class WalletPresenterFactory
    {
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public WalletPresenterFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        // создаём презентер кошелька с валютами
        public WalletPresenter CreateWalletPresenter(IconWithTextListView view)
            => new WalletPresenter(_walletService, view, this);

        // создаём презентер валют
        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresenter(_walletService.GetCurrency(currencyType), currencyType, view, _configsProviderService.CurrencyIconsConfig);
    }
}
