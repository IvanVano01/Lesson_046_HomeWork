using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class MoveToPosition 
    {  
        public void MoveTo(Transform transformObj, Vector3 position)
        {
            transformObj.position = position;
        }
    }
}
