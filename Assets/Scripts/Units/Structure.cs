using Panteon.Data;
using UnityEngine;

namespace Panteon.Units
{
    public class Structure : Unit, IStructure
    {
        protected StructureTemplate _structerTemplate => _unitTemplate as StructureTemplate;

        public void CreateUnit(string name, Vector2Int cellPosition)
        {
            Debug.Log($"Create {name} Unit");
        }

        public UnitTemplate GetUnitTemplate(string unitName)
        {
            return _structerTemplate.Productions.Find(u => u.UnitName == unitName);
        }
    }
}
