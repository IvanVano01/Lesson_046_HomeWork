using Assets.HomeWork.Develop.GamePlay.AI.Sensors;
using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Extensions;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.DamageFeature
{
    public class DealDamageOnSelfTriggerBehaviour : IEntityInitialize, IEntityDispose// нанесение урона врагу при сталкновении с колайдером врага
                                                                                     // на котором есть компонент "Entity"
    {
        private TriggerReciever _triggerReciever;
        private ReactiveVariable<float> _damage;

        private IDisposable _disposableTriggerEnter;

        public void OnInit(Entity entity)
        {
            _triggerReciever = entity.GetSelfTriggerReciever();
            _damage = entity.GetSelfTriggerDamage();

            _disposableTriggerEnter = _triggerReciever.Enter.Subcribe(OnTriggerEnter);// подписываемся на событие от "_triggerReciever.Enter"
        }

        private void OnTriggerEnter(Collider collider)
        {           

            Entity otherEntity = collider.GetComponentInParent<Entity>();// проверяем есть ли у колайдера с которым столкнулись,
                                                                         // компоненты "Entity"
            if (otherEntity != null)
            {
                Debug.Log($"Нашёл врага, наношу урон!{otherEntity.name}");

                otherEntity.TryTakeDamage(_damage.Value);// логика урона определена в классе расширений "EntityExtensions"
            }
        }

        public void OnDispose()
        {
            _disposableTriggerEnter.Dispose();
        }
    }
}
