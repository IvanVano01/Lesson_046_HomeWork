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

            ProcessRegistrations();// ����������� ��� �����
            InitializeUI();//��� Popup UI
            Debug.Log($"��������� ������� ��� ����� {mainMenuInputArgs}");

            yield return new WaitForSeconds(1f);// ���������� ��������            

            Debug.Log($" �������� �������� ��� �����, ���������!");
            _isRegistrationReady = true;
        }

        private void InitializeUI()
        {
            // ---------------- ��� Popup UI---------------------------------------------------------------------//
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
            // ������ ����������� ��� ����� 
            _container.RegisterAsSingle(c => new LevelsMenuPopupFactory(c));

            _container.RegisterAsSingle(c => new WalletPresenterFactory(c));// ������� "WalletPresenterFactory" � ������� ���� ���������
           

            _container.RegisterAsSingle(c =>
            {
                MainMenuUIRoot mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");

                return Instantiate(mainMenuUIRootPrefab);
            }).NonLazy();

            _container.RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
            .CreateWalletPresenter(c.Resolve<MainMenuUIRoot>().WalletView)).NonLazy();// ��� ����������� � ���������� ����� �� ���������� ���� ����� �����
            

            _container.Initialize();// ��� �������� �������� "NonLazy"            
        }

        private void Update()
        {
            
        }
    }
}
