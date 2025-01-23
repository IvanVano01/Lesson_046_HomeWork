using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures;
using Assets.HomeWork.Develop.Utils.Extensions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay
{
    public class GamePlayTest : MonoBehaviour // временный класс, для теста в процессе разработки геймплэя
    { 
        private DIContainer _container;

        private Entity _ghost;
        private Entity _soulMage;

        public void StartProcess(DIContainer container)
        {
            _container = container;

            _ghost = _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.forward * -4);//спавним сущность            

            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.forward * 4);//спавним ещё сущность, для теста 
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.left * 4);//спавним ещё сущность, для теста
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.right * 4);//спавним ещё сущность, для теста

            _soulMage = _container.Resolve<EntityFactory>().CreateSoulMage(Vector3.zero);
        }

        private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            //if (_ghost != null)
            //{                 
            //    _ghost.GetMoveDirection().Value = input;
            //    _ghost.GetRotationDirection().Value = input;

            //    if (Input.GetKeyDown(KeyCode.F) && _ghost.TryGetTakeDamageRequest(out var takeDamageRequest))
            //    {
            //        takeDamageRequest.Invoke(100);
            //        Debug.Log($"Осталось здоровья: " + _ghost.GetHealth().Value);
            //    }
            //}

            if (_soulMage != null)
            {  
                if (Input.GetKeyDown(KeyCode.F) && _soulMage.TryGetTakeDamageRequest(out var takeDamageRequest))
                {
                    takeDamageRequest.Invoke(100);
                    Debug.Log($"Осталось здоровья: " + _soulMage.GetHealth().Value);
                }

                if (Input.GetKeyDown(KeyCode.T) && _soulMage.TryGetTryToTeleportEvent(out var tryToTeleportEvent))
                {
                    tryToTeleportEvent.Invoke(_soulMage.GetTeleportationEnergyPrice().Value);

                    Debug.Log($"Осталось энергии для телепортации: " + _soulMage.GetTeleportationEnergy().Value);
                    Debug.Log($"Осталось здоровья: " + _soulMage.GetHealth().Value);
                }
            }
        }
    }
}
