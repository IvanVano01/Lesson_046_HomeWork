using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class TeleportationView : EntityView
    {
        [SerializeField] private ParticleSystem _teleptViewPrefab;
       
        private ReactiveEvent<Vector3> _goToTeleportEvent;

        private Transform _transform;        
        
        private IDisposable _disposableGoToTeleportEvent;

        protected override void OnEntityInitialized(Entity entity)
        {
            _transform = entity.GetTransform();
            
            _goToTeleportEvent = entity.GetGoToTeleportEvent();           
            _disposableGoToTeleportEvent = _goToTeleportEvent.Subcribe(OnGoToTeleportEvent);
        }

        protected override void OnEntityDisposed(Entity entity)
        {            
            _disposableGoToTeleportEvent.Dispose();
        }

        private void OnGoToTeleportEvent(Vector3 vector)
        {
            OnPlayTeleportVFX(vector,_transform.position);            
        }

        private void OnPlayTeleportVFX(Vector3 position1, Vector3 position2)
        {
            ParticleSystem view1 = Instantiate(_teleptViewPrefab, position1, Quaternion.identity, null);
            ParticleSystem view2 = Instantiate(_teleptViewPrefab, position2, Quaternion.identity, null);
        }
    }
}
