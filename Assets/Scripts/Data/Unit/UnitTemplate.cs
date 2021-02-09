using UnityEngine;

namespace Panteon.Data
{
    [CreateAssetMenu(fileName = "UnitTemplate", menuName = "panteon-demo/UnitTemplate", order = 0)]
    public class UnitTemplate : ScriptableObject
    {
        [SerializeField] private string _unitName;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector2Int _cellSize = Vector2Int.one;

        public Sprite Sprite => _sprite;
        public string UnitName => _unitName;
        public Vector2Int CellSize => _cellSize;
    }
}