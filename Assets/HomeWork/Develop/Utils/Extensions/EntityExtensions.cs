using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.Utils.Reactive;

namespace Assets.HomeWork.Develop.Utils.Extensions
{
    public static class EntityExtensions// класс экстеншенов
    {
        public static bool TryTakeDamage(this Entity entity, float damage)
        {

            if (entity.TryGetTakeDamageRequest(out ReactiveEvent<float> damageRequest))
            {
                damageRequest?.Invoke(damage);
                return true;
            }

            return false;
        }


        //--------------------------------------------------------------------------------------------//
        //public static Entity AddMoveSpeed(this Entity entity, ReactiveVariable<float> value)        
        //  => entity.AddValues(EntityValues.MoveSpeed, value);          

        //public static ReactiveVariable<float> GetMoveSpeed(this Entity entity)
        //{
        //    return entity.GetValue<ReactiveVariable<float>>(EntityValues.MoveSpeed);
        //}
        //--------------------------------------------------------------------------------------------//
    }
}
