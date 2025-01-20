using Assets.HomeWork.Develop.GamePlay.Entities;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.AI.Sensors
{
    public class SelfTriggerRecieverRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private TriggerReciever _triggerReciever;

        public override void Register(Entity entity)
        {
            entity.AddSelfTriggerReciever(_triggerReciever);
        }
    }
}
