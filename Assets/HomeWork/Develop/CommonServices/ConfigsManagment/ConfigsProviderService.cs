using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.Configs.Common.Wallet;
using Assets.HomeWork.Develop.Configs.GamePlay;

namespace Assets.HomeWork.Develop.CommonServices.ConfigsManagment
{
    public class ConfigsProviderService
    {
        private ResourcesAssetLoader _resourcesAssetLoader;

        public ConfigsProviderService(ResourcesAssetLoader resourcesAssetLoader)
        {
            _resourcesAssetLoader = resourcesAssetLoader;
        }

        public StartWalletConfig StartWalletConfig { get; private set; }       
        public CurrencyIconsConfig CurrencyIconsConfig { get; private set; }
        public LevelListConfig LevelListConfig { get; private set; }       

        public void LoadAll()
        {           
            LoadStartWalletConfig();          
            LoadCurrencyIconsConfig();          
            LoadLevelListConfig();
        }       

        private void LoadStartWalletConfig()
        {
            StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");
        }    

        private void LoadCurrencyIconsConfig()
        {
            CurrencyIconsConfig = _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");
        } 
        
        private void LoadLevelListConfig()
        {
            LevelListConfig = _resourcesAssetLoader.LoadResource<LevelListConfig>("Configs/GamePlay/Levels/LevelListConfig");
        }
    }
}
