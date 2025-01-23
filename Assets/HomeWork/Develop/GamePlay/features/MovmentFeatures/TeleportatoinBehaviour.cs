using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class TeleportatoinBehaviour : IEntityInitialize, IEntityDispose//, IEntityUpdate
    {
        private ReactiveEvent<float> _tryToTeleportEvent;
        private ReactiveEvent<Vector3> _goToTeleportEvent;

        private Transform _transform;
        private Vector3 _oldPosition;
        private Vector3 _randomPosition;
        private MoveToPosition _moveToPosition;
        private RandomGeneratorPosition _randomGeneratorPosition;

        private ReactiveVariable<Vector2> _teleportationCenterArea;
        private ReactiveVariable<float> _teleportationRadius;
        private ReactiveVariable<float> _teleportationEnergy;

        private ReactiveVariable<Vector3> _rotationDirection;

        private ICondition _condition;
        private IDisposable _disposableTryToTeleportEvent;

        private Coroutine _delayCoroutine;

        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _moveToPosition = entity.GetMoveToPosition();
            _randomGeneratorPosition = entity.GetRandomGeneratorPosition();
            _rotationDirection = entity.GetRotationDirection();

            _teleportationCenterArea = entity.GetTeleportationCenterArea();
            _teleportationRadius = entity.GetTeleportationRadius();
            _teleportationEnergy = entity.GetTeleportationEnergy();

            _tryToTeleportEvent = entity.GetTryToTeleportEvent();
            _goToTeleportEvent = entity.GetGoToTeleportEvent();

            _condition = entity.GetTeleportationCondition();
            _disposableTryToTeleportEvent = _tryToTeleportEvent.Subcribe(OnTakeTeleportation);
        }

        public void OnDispose()
        {
            _disposableTryToTeleportEvent.Dispose();
        }

        private void OnTakeTeleportation(float teleportationEnergy)
        {
            if (teleportationEnergy < 0)
                throw new ArgumentOutOfRangeException(nameof(teleportationEnergy));

            if (_condition.Evaluate() == false)
                return;

            _oldPosition = _transform.position;

            float tempTeleportationEnergy = _teleportationEnergy.Value - teleportationEnergy;
            _teleportationEnergy.Value = Math.Max(tempTeleportationEnergy, 0);// клэмпим , что бы энергия не опускалась ниже ноля

            _randomPosition = ToGenerateRandomPosition();

            _delayCoroutine = _transform.GetComponent<MonoBehaviour>().StartCoroutine(TimeDelay());
        }

        private IEnumerator TimeDelay()
        {
            Vector3 direction = _randomPosition - _oldPosition;
            _rotationDirection.Value = direction;

            yield return new WaitForSeconds(0.5f);

            ToTeleportation(_transform, _randomPosition);
            _goToTeleportEvent.Invoke(_oldPosition);
        }

        private void ToTeleportation(Transform transformObj, Vector3 position)
        {           
            _moveToPosition.MoveTo(transformObj, position);
        }

        private Vector3 ToGenerateRandomPosition() => _randomGeneratorPosition.ToGeneratePosition(
            _teleportationCenterArea.Value, _teleportationRadius.Value);
    }
}
