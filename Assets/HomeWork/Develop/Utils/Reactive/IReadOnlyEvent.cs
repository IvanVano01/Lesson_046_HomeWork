using System;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public interface IReadOnlyEvent// для доступа на чтение
    {
        IDisposable Subcribe(Action action);
    }

    public interface IReadOnlyEvent<T>// для доступа на чтение для дженерик
    {
        IDisposable Subcribe(Action<T> action);
    }
}
