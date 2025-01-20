using Assets.HomeWork.Develop.GamePlay.Entities;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class CharacterControllerRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CharacterController _characterController;   
        
        public override void Register(Entity entity)
        {
            entity.AddValue(EntityValues.CharacterController, _characterController);
        }
    }
}
