using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public class ReactiveEvent : IReadOnlyEvent// будем подписывать этот класс на что-либо и передовать экземпляр этого класса
    {

        private List<ActionNode> _subscribers = new();// список подписчиков

        public IDisposable Subcribe(Action action) // метод подписки в который, будем передавать кое-либо действия для его регистрации и
                                                   // возвращать будет"ActionNode" для отписки от этого действия
        {
            ActionNode actionNode = new ActionNode(action, Remove);// создаём экземпляр ноды события
            _subscribers.Add(actionNode); // регистрируем действие в списке "_subscribers"
            return actionNode;
        }

        public void Invoke()// вызов события
        {
            foreach (ActionNode subscriber in _subscribers)
                subscriber.Invoke();
        }

        private void Remove(ActionNode actionNode) // для удаление из списка зарегестрированного действия
            => _subscribers.Remove(actionNode);
    }

    public class ReactiveEvent<T> : IReadOnlyEvent<T>// то же самое только с передачей параметров(будем подписывать этот класс на что-либо и передовать экземпляр этого класса)
    {
        private List<ActionNode<T>> _subscribers = new();// список подписчиков

        public IDisposable Subcribe(Action<T> action) // метод подписки в который, будем передавать кое-либо действия для его регистрации и
                                                      // возвращать будет"ActionNode" для отписки от этого действия
        {
            ActionNode<T> actionNode = new ActionNode<T>(action, Remove);// создаём экземпляр ноды события
            _subscribers.Add(actionNode); // регистрируем действие в списке "_subscribers"
            return actionNode;
        }

        public void Invoke(T arg)// вызов события
        {
            foreach (ActionNode<T> subscriber in _subscribers)
                subscriber.Invoke(arg);
        }

        private void Remove(ActionNode<T> actionNode) // для удаление из списка зарегестрированного действия
            => _subscribers.Remove(actionNode);
    }
    
}
