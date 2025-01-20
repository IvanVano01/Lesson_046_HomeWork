using Assets.HomeWork.Develop.CommonUI;
using UnityEngine;

namespace Assets.HomeWork.Develop.MainMenu.UI
{
    public class MainMenuUIRoot : MonoBehaviour
    {
        [field: SerializeField] public ActionButton OpenLevelsMenuButton { get; private set; }

        [field: SerializeField] public IconWithTextListView WalletView { get; private set; }

        [field: SerializeField] public Transform HUDLayer { get; private set; }

        [field: SerializeField] public Transform PopupsLayer { get; private set; } //слой с окнами(магазины и т.д)

        [field: SerializeField] public Transform VFXLayer { get; private set; }

    }
}
