using System;

namespace Panteon.UI
{
    public class ProductionItemController
    {
        private ProductionItem _model;
        private ProductionItemView _view;

        public event Action<ProductionItem> Selected;
        public ProductionItemController(ProductionItem model, ProductionItemView view)
        {
            _view = view;
            _model = model;
            _view.ItemClicked += View_Clicked;
            _view.UpdateView(_model.Sprite, _model.Name);
        }

        private void View_Clicked()
        {
            Selected.Invoke(_model);
        }

        public void ChangeModel(ProductionItem model)
        {
            _model = model;
            _view.UpdateView(_model.Sprite, _model.Name);
        }
    }
}