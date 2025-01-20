using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    [RequireComponent(typeof(Animator))]
    public class MovementView : EntityView // класс будет отвечать за отображение бега сущности
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsWalking");// хешируем

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isMoving;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();// "??" - эта проверка означает что "GetComponent<Animator>()" будет вызываться
                                                   // только когда "_animator" = null
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _isMoving = entity.GetIsMoving();
            _isMoving.Changed += OnIsMovingChanged;// подписываемся на изменение
        }

        //private void OnIsMovingChanged(bool arg1, bool isMoving) => _animator.SetBool(IsMovingKey, isMoving);
        private void OnIsMovingChanged(bool arg1, bool isMoving)
        {
            if (isMoving)
                StartMove();
            else
                StopMove();
        }

        private void StopMove() => _animator.SetBool(IsMovingKey, false);        

        private void StartMove() => _animator.SetBool(IsMovingKey, true);

        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _isMoving.Changed -= OnIsMovingChanged;// отписываемся
        }
    }
}
