using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.Deathfeature
{
    [RequireComponent(typeof(Animator))]
    public class DeadView : EntityView
    {
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isDead;
        private ReactiveVariable<bool> _isDeadProcess;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialized(Entity entity)
        {
            _isDeadProcess = entity.GetIsDeathProcess();
            _isDead = entity.GetIsDead();

            _isDead.Changed += OnIsDeadChanged;
        }

        public void OnDeadAnimationEnded() // метод с коллБэком для подписки на событие от аниматора,
                                           // метод будем дёргать из аниматора
        {
            _isDeadProcess.Value = false;
        }

        private void OnIsDeadChanged(bool arg1, bool isDead) => _animator.SetBool(IsDeadKey, isDead);

        protected override void OnEntityDisposed(Entity entity)// для отписки
        {
            base.OnEntityDisposed(entity);

            _isDead.Changed -= OnIsDeadChanged;
        }
    }
}
