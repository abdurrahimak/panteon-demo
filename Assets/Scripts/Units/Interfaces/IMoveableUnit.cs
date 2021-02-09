using UnityEngine;

namespace Panteon.Units
{
    public interface IMoveableUnit : IUnit
    {
        void Move(Vector2Int cellPosition);
    }
}
