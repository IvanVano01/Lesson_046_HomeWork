using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DI
{
    public class DIContainer : IDisposable
    {
        private readonly Dictionary<Type, Registration> _container = new(); // сам контейнер ввиде словаря

        private readonly DIContainer _parent; // поле для родительского контейнера

        private readonly List<Type> _requests = new();//временный список будет хранить обращения к зависимостям, для обработки циклических зависимостей

        public DIContainer() : this(null)
        {

        }

        public DIContainer(DIContainer parent)
        {
            _parent = parent;
        }

        public Registration RegisterAsSingle<T>(Func<DIContainer, T> creator) // регаем объект в единственном экземпляре
        {
            if (IsAlreadyRegister<T>())// проверка есть ли уже такая регистрация в контейнере
                throw new InvalidOperationException($" {nameof(T)} Already register!");

            Registration registration = new Registration(container => creator(container));// создаём регистрацию 
            
            _container.Add(typeof(T), registration);// добавляем регистрацию в контейнер
            //_container[typeof(T)] = registration;// вариант добавления в словарь

            return registration;
        } 

        public bool IsAlreadyRegister<T>()
        {
            if(_container.ContainsKey(typeof(T)))// проверка в локальном контейнере, если такая регистрация уже есть
                return true;

            if(_parent != null)
                return _parent.IsAlreadyRegister<T>();// проверяем в глобальном контейнере

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))// если список уже хранит такую зависимость
                throw new InvalidOperationException($" Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));// добавляем обращение к зависимости

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))// проверили что контейнер содержит запись о запрашиваемой регистрации
                    return CreateFrom<T>(registration);

                if (_parent != null)// если в локальном контейнере нет записи регистрации, запрашиваем у родительского контейнера
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));// удаляем из списка
            }

            throw new InvalidOperationException($" Registration for {typeof(T)} not exist");
        }

        public void Initialize() // метод инициализации контейнера
        {
            foreach(Registration registration in _container.Values)// проходимся по каждой регистрации
            {
                if (registration.Instance == null && registration.IsNonLazy)// если регистрация помечена маркером "IsNonLazy"
                    registration.Instance = registration.Creator(this);     // то создаём эту регистрации, сразу и засовываем в поле "Instance"
                
                if(registration.Instance != null)
                    if(registration.Instance is IInitializable initializable) // проверяем, помечена ли регистрация интерфейсом "IInitializable"
                        initializable.Inintialize();                           
            }            
        }

        public void Dispose() // для отписки при переходе в другую сцену и т.д
        {
            foreach (Registration registration in _container.Values)// проходимся по каждой регистрации
            {
                if (registration.Instance != null)
                    if (registration.Instance is IDisposable disposable) // проверяем, помечена ли регистрация интерфейсом "IDisposable"
                        disposable.Dispose();
            }
        }

        private T CreateFrom<T>(Registration registration)// метод будет принимать созданную регистрацию из контейнера и записывать в поле "Instance" и возвращать её
        {
            if (registration.Instance == null && registration.Creator != null)// проверяем что мы ещё не создавали такую регистрацию
                registration.Instance = registration.Creator(this);

            return (T)registration.Instance;
        }

        public class Registration
        {
            public Func<DIContainer, object> Creator { get; }// делегат принимает контейнер, возвращает созданный объект

            public object Instance { get; set; }

            public bool IsNonLazy { get; private set; } // маркер ленивого создания объекта, т.е ещё до первого вызова объект будет создан

            public Registration(object instance)
            {
                Instance = instance;
            }

            public Registration(Func<DIContainer, object> creator)
            {
                Creator = creator;
            }

            public void NonLazy() => IsNonLazy = true;
        }
    }
}
