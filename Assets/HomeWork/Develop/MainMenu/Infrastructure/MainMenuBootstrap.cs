using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using Assets.HomeWork.Develop.CommonUI.Wallet;
using Assets.HomeWork.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup;
using Assets.HomeWork.Develop.MainMenu.UI;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private DIContainer _container;        

        private bool _isRegistrationReady;

        public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
        {
            _container = container;

            ProcessRegistrations();// регистрации для сцены
            InitializeUI();//для Popup UI
            Debug.Log($"Подружаем ресурсы для сцены {mainMenuInputArgs}");

            yield return new WaitForSeconds(1f);// симулируем ожидание            

            Debug.Log($" Загрузка ресурсов для сцены, заверщена!");
            _isRegistrationReady = true;
        }

        private void InitializeUI()
        {
            // ---------------- для Popup UI---------------------------------------------------------------------//
            MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            mainMenuUIRoot.OpenLevelsMenuButton.Initialize(() =>
            {
                LevelsMenuPopupPresenter levelsMenuPopupPresenter = _container.Resolve<LevelsMenuPopupFactory>().CreateLevelsMenuPopupPresenter();
                levelsMenuPopupPresenter.Enable();
            });
            // ------------------------------------------------------------------------------------------------//
        }

        private void ProcessRegistrations()
        {
            // Делаем регистрации для сцены 
            _container.RegisterAsSingle(c => new LevelsMenuPopupFactory(c));

            _container.RegisterAsSingle(c => new WalletPresenterFactory(c));// создаем "WalletPresenterFactory" и передаём туда контейнер
           

            _container.RegisterAsSingle(c =>
            {
                MainMenuUIRoot mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");

                return Instantiate(mainMenuUIRootPrefab);
            }).NonLazy();

            _container.RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
            .CreateWalletPresenter(c.Resolve<MainMenuUIRoot>().WalletView)).NonLazy();// для отображения и обновления валют на протяжении всей жизни сцены
            

            _container.Initialize();// для создания объектов "NonLazy"            
        }

        private void Update()
        {
            
        }
    }
}
