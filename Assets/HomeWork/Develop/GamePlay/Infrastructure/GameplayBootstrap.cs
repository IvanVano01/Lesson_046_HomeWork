using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using Assets.HomeWork.Develop.GamePlay.Entities;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;
        private GameplayInputArgs _gameplayInputArgs;

        [SerializeField] private GamePlayTest _gameplayTest;// временно для тестирования
       

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;
            _gameplayInputArgs = gameplayInputArgs;

            ProcessRegistrations();

            Debug.Log($"Подружаем ресурсы для режима игры под номером: {gameplayInputArgs.LevelNumber}");
            Debug.Log("Сцена готова, можно начинать игру! ");

            _gameplayTest.StartProcess(_container);
            yield return new WaitForSeconds(1f);// симулируем ожидание        

           
        }

        private void ProcessRegistrations()
        {
            // регаем всё что нужно для этой сцены
 
            _container.RegisterAsSingle(c => new EntityFactory(c)); 

            _container.Initialize();// для создания объектов "NonLazy"
        }        
    }
}
