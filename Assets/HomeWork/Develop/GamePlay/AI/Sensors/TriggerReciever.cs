﻿using Assets.HomeWork.Develop.Utils.Reactive;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.AI.Sensors
{
    [RequireComponent(typeof(Collider))]
    public class TriggerReciever : MonoBehaviour // класс будет отслеживать касание колайдеров с другими объектами и вызывать событие
    {
        [SerializeField] private List<Collider> _ignoreColliders;// список игнорируемых колайдеров

        private ReactiveEvent<Collider> _enter = new();
        private ReactiveEvent<Collider> _exit = new();
        private ReactiveEvent<Collider> _stay = new();

        public IReadOnlyEvent<Collider> Enter => _enter;//св-во для подписки на событие
        public IReadOnlyEvent<Collider> Exit => _exit;
        public IReadOnlyEvent<Collider> Stay => _stay;
       
        public IReadOnlyList<Collider > IgnoreColliders => _ignoreColliders;

        private void Awake()
        {
            Collider selfCollider = GetComponent<Collider>();
            
            foreach(Collider collider in _ignoreColliders)
            {
                Physics.IgnoreCollision(selfCollider, collider);// что бы объект не детектил свои колайдеры 
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _enter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _exit.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            _stay.Invoke(other);
        }        
    }
}
