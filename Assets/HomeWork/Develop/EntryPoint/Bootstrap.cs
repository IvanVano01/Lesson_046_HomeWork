using Assets.HomeWork.Develop.CommonServices.ConfigsManagment;
using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.LoadingScreen;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            ILoadingCurtain loadingCurtain = container.Resolve<ILoadingCurtain>();
            SceneSwitcher sceneSwitcher = container.Resolve<SceneSwitcher>();// берём из контейнера "SceneSwitcher" для перехода в другую сцену

            loadingCurtain.Show(); //Включаем загрузочную штору 

            Debug.Log("Начинается инициализация сервисов");

            // Инициализация всех сервисов(конфиги, инит сервисы рекламы/ аналитики)
            container.Resolve<ConfigsProviderService>().LoadAll();// подгрузили конфиги            
            container.Resolve<PlayerDataProvider>().Load();// подгружаем данные для игрока из конфигов
              
            

            yield return new WaitForSeconds(1.5f);// заглушка, имитирует инициализацию сервисов которые выше

            Debug.Log("Все сервисы проекта инициализированны! Переходим на следующую сцену.");
            loadingCurtain.Hide(); // скрываем штору
            
            // переход на следующую сцену с помощью сервиса смены сцены

           //sceneSwitcher.ProcessSwitchSceneFor( new OutputBootstrapArgs( new MainMenuInputArgs()));// из сцены "Bootstrap" переходим в "MainMenu"
           
            sceneSwitcher.ProcessSwitchSceneFor(new OutputBootstrapArgs(new GameplayInputArgs(1)));// временно для перехода сразу в сцену геймплэя
        }
    }
}
