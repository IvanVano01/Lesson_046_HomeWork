using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;

namespace Assets.HomeWork.Develop.GamePlay.features.DamageFeature
{
    public class ApplyDamageFillterBehaviour : IEntityInitialize, IEntityDispose// фильтр для состояния, может ли сущность получить урон
    {
        private ReactiveEvent<float> _takeDamageEvent;//само событие нанесения урона
        private ReactiveEvent<float> _takeDamageRequest;//событие фильтр, будет проверять можно ли нанести урон
        private ICondition _takeDamageCondition;

        private IDisposable _disposableTakeDamageRequest;// // поле для сохранения ссылки для отписки от реактивного события

        public void OnInit(Entity entity)
        {
            _takeDamageCondition = entity.GetTakeDamageCondition();
            _takeDamageRequest = entity.GetTakeDamageRequest();
            _takeDamageEvent = entity.GetTakeDamageEvent();

            _disposableTakeDamageRequest = _takeDamageRequest.Subcribe(OnTakeDamageRequest);// подписываемся на реактивное событие
                                                                                            // и сохраняем ссылку на него для отписки
        }

        private void OnTakeDamageRequest(float damage) // метод реактивного события
        {
            if(damage <0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if(_takeDamageCondition.Evaluate())// проверяем, можно ли наносить сейчас урон сущности
                _takeDamageEvent.Invoke(damage);// вызываем событие для нанесения урона

        }

        public void OnDispose()
        {
            _disposableTakeDamageRequest.Dispose();// отписываемся от реактивного события
        }
    }
}
