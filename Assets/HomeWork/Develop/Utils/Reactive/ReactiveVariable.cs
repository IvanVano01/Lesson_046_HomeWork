using System;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public class ReactiveVariable<T> : IReadOnlyVariable<T> where T : IEquatable<T> // реактивное сво-во
    {
        public event Action<T, T> Changed;// событие для подписки на изменение  переменной

        private T _value;// сама переменная 

        public ReactiveVariable() => _value = default(T); //конструктор 1
        public ReactiveVariable(T value) => _value = value; // конструктор 2

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;
                _value = value;

                if (_value.Equals(oldValue) == false)
                    Changed?.Invoke(oldValue, value);
            }
        }
    }
}
