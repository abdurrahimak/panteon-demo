using CoreProject.Singleton;
using CoreProject.States;
using Panteon.States;
using Panteon.UI;
using Panteon.Units;

namespace Panteon.Creation
{
    public class StateFactory : SingletonClass<StateFactory>
    {
        public IGameState CreateStructureCreationState(IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel)
        {
            return new StructureCreationState(parentGameStateController, productionPanel, informationPanel);
        }

        public IGameState CreateStructureState(IStructure structure, IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel)
        {
            return new StructureState(structure, parentGameStateController, productionPanel, informationPanel);
        }

        public IGameState CreateMovableUnitState(IMoveableUnit movableUnit, IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel)
        {
            return new MovableUnitState(movableUnit, parentGameStateController, productionPanel, informationPanel);
        }
    }
}