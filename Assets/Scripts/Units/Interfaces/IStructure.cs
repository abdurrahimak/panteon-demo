

using Panteon.Data;

namespace Panteon.Units
{
    public interface IStructure : IUnit
    {
        UnitTemplate GetUnitTemplate(string name);
    }
}
