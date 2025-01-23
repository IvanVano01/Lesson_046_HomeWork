using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class RandomGeneratorPosition 
    { 
        public Vector3 ToGeneratePosition(Vector2 centerArea, float radiusArea)
        {            
            Vector2 randomPoint = (UnityEngine.Random.insideUnitCircle * radiusArea) + centerArea;
            Vector3 newPosition = new Vector3(randomPoint.x,0,randomPoint.y);
           
            return newPosition;
        }
    }
}
