using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.HomeWork.Develop.Utils.Conditions
{
    public class CompositeCondition : ICompositeCondition // реализания композитного условия
    {
        private List<(ICondition, Func<bool, bool, bool>)> _conditions = new();// лист будет хранить результат лог.операции для каждого условия(сondition)

        private Func<bool, bool, bool> _standardLogigOperation;// делегат будет принимать 2 аргуметна типа"bool" и
                                                               // возвращать результат логич.операии этих переменных в виде переменной "bool" 
        //пример что будет делать делегат
        //private bool LogigOperation(bool arg1,bool arg2) => arg1 && arg2;

        public CompositeCondition(Func<bool, bool, bool> standardLogigOperation)// конструктор 1 для стандартной логич. операции
        {
            _standardLogigOperation = standardLogigOperation;
        }

        public CompositeCondition(ICondition condition, Func<bool, bool, bool> standardLogigOperation) : this(standardLogigOperation)  // конструктор 2
        {
            _conditions.Add((condition, standardLogigOperation));
        }

        public bool Evaluate()
        {
            if(_conditions.Count == 0) 
                return false;

            bool result = _conditions[0].Item1.Evaluate();// берём первый логический элемент"[0]" из списка"_conditions" и запоминаем его значение

            // далее циклом проходимя по остальным логическим элементам и производим логические операции(&&,|| и т.д) по очерёдно,
            // запоминая результат предыдущей и сравнивая со следующей
            for (int i =1 ; i < _conditions.Count; i++)
            {
                var currentCondition = _conditions[i];

                if (currentCondition.Item2 != null)// если у элемента есть переопределение для логической операции
                    result = currentCondition.Item2.Invoke(result, currentCondition.Item1.Evaluate());
                else                                // если у элемента нет переопределение для логической операции
                    result = _standardLogigOperation.Invoke(result, currentCondition.Item1.Evaluate());
            }

            return result;
        }

        public ICompositeCondition Add(ICondition condition, Func<bool, bool, bool> logicOperation = null)
        {
            _conditions.Add((condition, logicOperation));
            return this;
        }

        public ICompositeCondition Remove(ICondition condition)
        {
            var conditionPair = _conditions.First(condPair => condPair.Item1 == condition);
             
            _conditions.Remove(conditionPair);
            return this;
        }
    }
}
