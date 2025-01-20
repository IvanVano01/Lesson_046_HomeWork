namespace Assets.HomeWork.Develop.Utils.Conditions
{
    public static class LogicOperations
    {
        // метод который будет соответствовать фанку "Func<bool, bool, bool>", принимает две булки и возвращает третью "bool",
        // как результат логич. операции между двумя первыми
        public static bool AndOperation(bool previous, bool current ) => previous && current;

        public static bool OrOperation(bool previous, bool current ) => previous || current;
    }
}
