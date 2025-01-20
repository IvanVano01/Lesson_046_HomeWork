using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.UI
{
    public class GamePlayUIRoot : MonoBehaviour
    { 
        [field: SerializeField] public Transform HUDLayer { get; private set; }

        [field: SerializeField] public Transform PopupsLayer { get; private set; } 

        [field: SerializeField] public Transform VFXLayer { get; private set; }
    }
}
