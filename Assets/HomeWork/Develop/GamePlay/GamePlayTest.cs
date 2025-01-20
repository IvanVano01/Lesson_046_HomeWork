using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.Utils.Extensions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay
{
    public class GamePlayTest : MonoBehaviour // временный класс, для теста в процессе разработки геймплэя
    {
        private DIContainer _container;

        private Entity _ghost;

        public void StartProcess(DIContainer container)
        {
            _container = container;

            _ghost = _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero);//спавним сущность            

            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.forward * 4);//спавним ещё сущность, для теста 

            Debug.Log($"Скорость созданного призрака равна {_ghost.GetMoveSpeed().Value}");
        }

        private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (_ghost != null)
            {
                //_ghost.TryGetValue(EntityValues.MoveDirection, out ReactiveVariable<Vector3> moveDirection);
                //_ghost.TryGetValue(EntityValues.RotationDirection, out ReactiveVariable<Vector3> rotationDirection);                

                _ghost.GetMoveDirection().Value = input;
                _ghost.GetRotationDirection().Value = input;

                if (Input.GetKeyDown(KeyCode.F) && _ghost.TryGetTakeDamageRequest(out var takeDamageRequest))
                {
                    takeDamageRequest.Invoke(100);
                    Debug.Log($"Осталось здоровья: " + _ghost.GetHealth().Value);
                }
            }
        }
    }
}
