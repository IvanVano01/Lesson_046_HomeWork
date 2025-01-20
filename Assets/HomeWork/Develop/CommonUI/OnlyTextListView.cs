using System.Collections.Generic;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonUI
{
    public class OnlyTextListView : MonoBehaviour // контейнер для вьюх "OnlyText"
    {
        [SerializeField] private OnlyText _onlyTextPrefab;
        [SerializeField] private Transform _parent;// контейнер

        private List<OnlyText> _elements = new();

        public OnlyText SpawnElement()
        {
            OnlyText onlyText = Instantiate(_onlyTextPrefab, _parent);
            _elements.Add(onlyText);
            return onlyText;
        }

        public void Remove(OnlyText onlyText)
        {
            _elements.Remove(onlyText);
            Destroy(onlyText.gameObject);
        }
    }
}
