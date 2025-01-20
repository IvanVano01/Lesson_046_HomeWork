using System;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public interface IReadOnlyVariable<T> // интерьфейс для выдачи реактивной переменной только на чтение
    {
        event Action<T, T> Changed;

        T Value { get; }
    }
}
