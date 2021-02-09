using System;
using System.Collections.Generic;

namespace Panteon.UI
{
    public interface IProductionPanel
    {
        event Action<ProductionItem> ProductionItemSelected;
        void SetModels(List<ProductionItem> productionItems);
        void Show(bool show);
    }
}