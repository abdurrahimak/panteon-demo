using System;
using System.Collections.Generic;
using Panteon.Creation;

namespace Panteon.UI
{
    public class ProductionPanel : IProductionPanel
    {
        private ProductionPanelView _view;

        private List<ProductionItemController> _productionItemControllers;

        public event Action<ProductionItem> ProductionItemSelected;

        public ProductionPanel()
        {
            _view = UIFactory.Instance.CreateProductionPanelView();

            InitializeItemControllers();
            Show(false);
        }

        private void InitializeItemControllers()
        {
            _productionItemControllers = new List<ProductionItemController>();
            foreach (var productionItemView in _view.ProductionItemViews)
            {
                ProductionItemController controller = new ProductionItemController(ProductionItem.Empty, productionItemView);
                controller.Selected += ProductionItem_Selected;
                _productionItemControllers.Add(controller);
            }
        }

        private void ProductionItem_Selected(ProductionItem productionItem)
        {
            ProductionItemSelected?.Invoke(productionItem);
        }

        public void SetModels(List<ProductionItem> productionItems)
        {
            for (int i = 0; i < _productionItemControllers.Count; i++)
            {
                var model = productionItems[i % productionItems.Count];
                _productionItemControllers[i].ChangeModel(model);
            }
        }

        private void ClearControllers()
        {
            foreach (var controller in _productionItemControllers)
            {
                controller.Selected -= ProductionItem_Selected;
            }
            _productionItemControllers.Clear();
        }

        public void Show(bool show)
        {
            _view.Show(show);
        }
    }
}
