using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.CommonServices.ConfigsManagment;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.LevelsService;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using Assets.HomeWork.Develop.MainMenu.UI;
using UnityEngine;

namespace Assets.HomeWork.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelsMenuPopupFactory
    {
        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _resourcesAssetLoader;
        private readonly MainMenuUIRoot _mainMenuUIRoot;

        public LevelsMenuPopupFactory(DIContainer container)
        {
            _container = container;
            _resourcesAssetLoader = _container.Resolve<ResourcesAssetLoader>();
            _mainMenuUIRoot =_container.Resolve<MainMenuUIRoot>();
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        {
            return new LevelTilePresenter(_container.Resolve<CompletedLevelsService>(),
                _container.Resolve<SceneSwitcher>(), levelNumber, view);
        }

        public LevelTileListpresenter CreateLevelTileListPresenter(LevelTileListView view)
        {
            return new LevelTileListpresenter(_container.Resolve<ConfigsProviderService>().LevelListConfig, this, view);
        }

        public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter()
        {
            //по идее "levelsMenuPopupView" не должен создаваться в "LevelsMenuPopupPresenter", но пока сделали так 
            LevelsMenuPopupView levelsMenuPopupViewPrefab = _resourcesAssetLoader.LoadResource<LevelsMenuPopupView>("MainMenu/UI/LevelsMenuPopup/LevelsMenuPopupView");
            LevelsMenuPopupView levelsMenuPopupView = Object.Instantiate(levelsMenuPopupViewPrefab, _mainMenuUIRoot.PopupsLayer);

            return new LevelsMenuPopupPresenter(this, levelsMenuPopupView);
        }
    }
}
