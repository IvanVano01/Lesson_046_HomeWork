using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class RotationBehaviour : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<float> _rotationSpeed;
        private IReadOnlyVariable<Vector3> _direction;

        private ICondition _condition;

        public void OnInit(Entity entity)
        {
            // запрашиваем у сущности нужные данные для реализации поведения

            //------------------------запись без автогенерации кода----------------------------//
            //_rotationSpeed = entity.GetValue<ReactiveVariable<float>>(EntityValues.RotationSpeed);
            //_direction = entity.GetValue<ReactiveVariable<Vector3>>(EntityValues.RotationDirection);
            //_transform = entity.GetValue<Transform>(EntityValues.Transform);
            //-----------------------------------------------------------------------------------//

            _rotationSpeed = entity.GetRotationSpeed();
            _direction = entity.GetRotationDirection();
            _transform = entity.GetTransform();

            _condition = entity.GetRotationCondition();// получаем у сущности условие при котором можно крутиться
        }

        public void OnUpdate(float deltaTime)
        {
            if (_direction.Value == Vector3.zero || _condition.Evaluate() == false)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_direction.Value.normalized);
            float step = _rotationSpeed.Value * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
        }
    }
}
