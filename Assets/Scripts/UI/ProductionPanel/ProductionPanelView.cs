using System;
using System.Collections.Generic;
using UnityEngine;

namespace Panteon.UI
{
    public class ProductionPanelView : MonoBehaviour
    {
        [SerializeField] private Transform _productionItemParent;

        private List<ProductionItemView> _productionItemViews;

        public event Action<ProductionItem> ClickedProductionItem;

        public List<ProductionItemView> ProductionItemViews => _productionItemViews;

        private void Awake()
        {
            _productionItemViews = new List<ProductionItemView>();
            for (int i = 0; i < _productionItemParent.childCount; i++)
            {
                _productionItemViews.Add(_productionItemParent.GetChild(i).GetComponent<ProductionItemView>());
            }
        }

        internal void Show(bool show)
        {
            GetComponent<Canvas>().enabled = show;
        }
    }
}