using System;

namespace Assets.HomeWork.Develop.Utils.Conditions
{
    public class FuncCondition : ICondition// реализация простого условия
    {
        private Func<bool> _condition;// будем передавать делегат, который будет возвращать true или false

        public FuncCondition(Func<bool> condition)
        {
            _condition = condition;
        }

        public bool Evaluate() => _condition.Invoke();// будет принимать значение, которое вернёт вызванный делегат "Func<bool> _condition"
    }
}
