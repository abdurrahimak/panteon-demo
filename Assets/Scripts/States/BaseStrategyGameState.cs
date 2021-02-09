using System;
using CoreProject.States;
using Panteon.UI;
using Panteon.Units;

namespace Panteon.States
{
    public abstract class BaseStrategyGameState : BaseGameState
    {
        protected IProductionPanel _productionPanel;
        protected IInformationPanel _informationPanel;
        public event Action<IUnit> UnitCreated;
        protected BaseStrategyGameState(IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel) : base(parentGameStateController)
        {
            _informationPanel = informationPanel;
            _productionPanel = productionPanel;
        }

        protected void OnUnitCreated(IUnit unit)
        {
            UnitCreated?.Invoke(unit);
        }
    }
}
