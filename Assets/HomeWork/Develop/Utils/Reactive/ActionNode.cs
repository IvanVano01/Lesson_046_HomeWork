using System;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public class ActionNode : IDisposable // класс для вызова события и отписки от него 
    {
        private Action _action;// само событие

        private Action<ActionNode> _onDispose;// событие для отписки

        public ActionNode(Action action, Action<ActionNode> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }

        public void Invoke() => _action?.Invoke();// вызываем событие
        public void Dispose() => _onDispose?.Invoke(this);// вызываем событие для отписки       
    }

    public class ActionNode<T> : IDisposable //то же самое только с передачей параметров(класс дженерик для вызова события и отписки от него)
    {
        private Action<T> _action;// само событие

        private Action<ActionNode<T>> _onDispose;// событие для отписки

        public ActionNode(Action<T> action, Action<ActionNode<T>> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }

        public void Invoke(T arg) => _action?.Invoke(arg);// вызываем событие
        public void Dispose() => _onDispose?.Invoke(this);// вызываем событие для отписки
    }
}
