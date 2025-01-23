using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class CharacterControllerMovmentBehaviour : IEntityInitialize, IEntityUpdate
    {
        private CharacterController _characterController;

        private IReadOnlyVariable<float> _speed;
        private IReadOnlyVariable<Vector3> _direction;
        private ReactiveVariable<bool> _isMoving;

        private ICondition _condition;

        public void OnInit(Entity entity)
        { 
            _speed = entity.GetMoveSpeed();
            _direction = entity.GetMoveDirection();
            _characterController = entity.GetCharacterController();

            _condition = entity.GetMoveCondition();// получаем у сущности условие при котором можно двигаться
            _isMoving = entity.GetIsMoving();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)// проверяем условие при котором можно двигаться
            {
                _isMoving.Value = false;
                return;
            }

            Vector3 velocity = _direction.Value.normalized * _speed.Value;
            _isMoving.Value = velocity.magnitude > 0;// двигаемся если длина вектора скорости больше ноля(для запуска анимации)

            _characterController.Move(velocity * deltaTime);
        }
    }
}
