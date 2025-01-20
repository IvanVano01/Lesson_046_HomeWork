using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;

namespace Assets.HomeWork.Develop.GamePlay.features.DamageFeature
{
    public class ApplyDamageBehaviour : IEntityInitialize, IEntityDispose // приминение урона для сущности
    {
        private ReactiveEvent<float> _takeDamageEvent;// реактивное событие
        private ReactiveVariable<float> _health;

        private IDisposable _disposableTakeDamageEvent; // поле для сохранения ссылки для отписки от реактивного события

        public void OnInit(Entity entity)
        {
            _takeDamageEvent = entity.GetTakeDamageEvent();// получаем реактивное событие
            _health = entity.GetHealth();

            _disposableTakeDamageEvent = _takeDamageEvent.Subcribe(OnTakeDamage);// подписываемся на реактивное событие
                                                                                 // и сохраняем ссылку на него для отписки
        }

        private void OnTakeDamage(float damage) // метод реактивного события
        {
            if(damage<0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            float tempHealth = _health.Value - damage;// считаем здоровье

            _health.Value = Math.Max(tempHealth, 0);//клэмпим здоровье, чтобы значение здоровья не было меньше ноля
        }

        public void OnDispose()
        {
            _disposableTakeDamageEvent.Dispose();// отписываемся от реактивного события
        }
    }
}
