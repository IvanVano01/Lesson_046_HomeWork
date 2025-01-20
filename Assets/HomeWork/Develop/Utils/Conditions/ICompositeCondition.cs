using System;

namespace Assets.HomeWork.Develop.Utils.Conditions
{
    public interface ICompositeCondition : ICondition 
    {
        ICompositeCondition Add(ICondition condition, Func<bool,bool,bool> logicOperation = null);// метод добавления условия

        ICompositeCondition Remove(ICondition condition);// метод удаления условия
    }
}
