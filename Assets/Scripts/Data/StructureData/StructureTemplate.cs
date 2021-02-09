using System.Collections.Generic;
using UnityEngine;

namespace Panteon.Data
{
    public enum StructureType
    {
        Barrack,
        PowerPlant,
        Home,
        Farm,
        Storage,
        Market
    }

    [CreateAssetMenu(fileName = "StructureTemplate.asset", menuName = "panteon-demo/StructureTemplate", order = 0)]
    public class StructureTemplate : UnitTemplate
    {
        [SerializeField] private StructureType _structureType;
        [SerializeField] private List<UnitTemplate> _productions;

        public StructureType StructureType => _structureType;
        public List<UnitTemplate> Productions => _productions;
    }
}