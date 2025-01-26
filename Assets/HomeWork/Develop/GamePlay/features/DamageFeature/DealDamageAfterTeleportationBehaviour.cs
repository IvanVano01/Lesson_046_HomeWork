using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Extensions;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.DamageFeature
{
    public class DealDamageAfterTeleportationBehaviour : IEntityInitialize, IEntityDispose // нанесение урона после телепортации
    {
        private ReactiveEvent <Vector3> _goToTeleportEvent;
        
        private Transform _transform;
        private List<Collider> _ignoreColliders;
        private ReactiveVariable<float> _damage;
        private ReactiveVariable<float> _damageRadius;              

        private IDisposable _disposableGoToTeleportEvent;

        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _damage = entity.GetTeleportationDamage();
            _damageRadius = entity.GetTeleportationDamageRadius();
            _ignoreColliders = entity.GetSelfTriggerReciever().IgnoreColliders.ToList();

            _goToTeleportEvent = entity.GetGoToTeleportEvent();
            _disposableGoToTeleportEvent = _goToTeleportEvent.Subcribe(OnGoToTeleport);
        }

        public void OnDispose()
        {
            _disposableGoToTeleportEvent .Dispose();
        }

        private void OnGoToTeleport(Vector3 vector)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _damageRadius.Value);
            
            foreach (Collider collider in colliders)
            {                
                if (_ignoreColliders.Contains(collider)) 
                    return;                    

                Entity otherEntity = collider.GetComponentInParent<Entity>();// проверяем есть ли у колайдера с которым столкнулись,
                                                                             // компоненты "Entity"
                if (otherEntity != null)
                {
                    Debug.Log($"Нашёл врага, наношу урон врагу {otherEntity.name}!");

                    otherEntity.TryTakeDamage(_damage.Value);// логика урона определена в классе расширений "EntityExtensions"
                }
            }
        }      
    }
}
