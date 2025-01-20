using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Entities
{
    public class Entity : MonoBehaviour // основа для любой сущности в рамках геймплэя,
                                        // будем её наполнять разными данными,свойствами и повидениями
    {
        public event Action<Entity> Initialized;// событие будет кричать что "Entity" проинициализировалось и
                                                // так же будет давать ссылку на само себя "Entity"
        public event Action<Entity> Disposed;// событие будет кричать что "Entity" задиспозилось

        private readonly Dictionary<EntityValues, object> _values = new();//словарь будет хранить зарегистрированные данные,
                                                                          //ключ(тип данных) это "enum EntityValues",
                                                                          //а сами данные "object"

        private readonly HashSet<IEntityBehaviour> _behaviours = new();// список типа "HashSet", который может содержать,
                                                                       // только уникальные елементы,                                                                       
                                                                       // список будет хранить все поведения

        private readonly List<IEntityUpdate> _updatables = new(); // список сущностей с "OnUpdate(float deltaTime)"
        private readonly List<IEntityInitialize> _initializables = new();// список сущностей с "OnInit(Entity entity)"
        private readonly List<IEntityDispose> _disposeables = new();

        private bool _isInit;

        private void Awake()
        {
            Install();
        }

        private void Install()// для получения компонентов на объекте
        {
            MonoEntityRegistrator[] registrators = GetComponents<MonoEntityRegistrator>();

            if (registrators != null)
            {
                foreach (MonoEntityRegistrator registrator in registrators)
                    registrator.Register(this);
            }
        }

        //-------------------------------------------------------------------//
        public void Initialize()
        {
            foreach(IEntityInitialize initializable in _initializables)            
                initializable.OnInit(this);// вызываем метод если поведение добавили во время игры
            
            _isInit = true;
            Initialized?.Invoke(this);// вызываем событие что проинициализировались и передаём себя
        }

        private void Update()
        {
            if (_isInit == false)
                throw new InvalidOperationException(" Update for not inited!");

            foreach(IEntityUpdate update in _updatables)
                update.OnUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            foreach(IEntityDispose desposable in _disposeables)
                desposable.OnDispose();

            Disposed?.Invoke(this); 
        }
        //-------------------------------------------------------------------//

        public Entity AddValue<TValue>(EntityValues valueType, TValue value)// метод добавления данных, возвращает сам класс"Entity"
        {
            if (_values.ContainsKey(valueType))
                throw new ArgumentException(valueType.ToString());

            _values.Add(valueType, value);
            return this;
        }

        public bool TryGetValue<TValue>(EntityValues valueType, out TValue value)// проверка запроса данных в словаре "_values",
                                                                                 // если данные есть есть в словаре, то возвращаем их 
        {
            if (_values.TryGetValue(valueType, out object findedObject))
            {
                if (findedObject is TValue findedValue)//проверяем что тип данных, тот что мы запросили по ключу
                {
                    value = findedValue;
                    return true;
                }
            }

            value = default(TValue);// если проверку не прошли
            return false;
        }

        public TValue GetValue<TValue>(EntityValues valueType)// метод получения данных
        {
            if (TryGetValue(valueType, out TValue value) == false)// если проверка запрашиваемых данных не прошла
                throw new ArgumentException($"Entity not exist {valueType}");

            return value;
        }

        public Entity AddBehaviour(IEntityBehaviour behaviour)// метод добавления поведений
        {
            if(_behaviours.Contains(behaviour))
                throw new ArgumentException(behaviour.GetType().ToString());

            _behaviours.Add(behaviour);

            // проверяем какой интерфейс реалезует сущность и добавляем в соответствующий список
            if (behaviour is IEntityUpdate updatable)
                _updatables.Add(updatable);

            if(behaviour is IEntityInitialize initializable)
            {
                _initializables.Add(initializable);

                if (_isInit)
                    initializable.OnInit(this);
            }

            if(behaviour is IEntityDispose disposeable)
                _disposeables.Add(disposeable);

            return this;
        }
    }
}
