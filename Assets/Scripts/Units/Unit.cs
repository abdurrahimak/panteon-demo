using UnityEngine;
using System;
using CoreProject.Pool;
using Panteon.Data;

namespace Panteon.Units
{
    public class Unit : PoolObject, IUnit
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected UnitTemplate _unitTemplate;
        protected Vector2Int _cellPosition;
        protected Vector2Int _cellSize;

        public string Name => _unitTemplate.UnitName;

        public Sprite Sprite => _unitTemplate.Sprite;

        public Vector2Int CellSize => _cellSize;

        public Vector2Int CellPosition => _cellPosition;

        protected event Action<IUnit> Selected;

        event Action<IUnit> IUnit.Selected
        {
            add => Selected += value;
            remove => Selected -= value;
        }

        private void Awake()
        {
            _cellSize = _unitTemplate.CellSize;
        }

        private void Start()
        {
            transform.localScale = new Vector3(CellSize.x, CellSize.y, 1f);
        }

        public UnitTemplate GetTemplate()
        {
            return _unitTemplate;
        }

        public void Select()
        {
            Selected?.Invoke(this);
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetCellPosition(Vector2Int cellPosition)
        {
            _cellPosition = cellPosition;
        }

        public void Turn()
        {
            _spriteRenderer.transform.Rotate(new Vector3(0f, 0, 90f));
            int tempX = _cellSize.x;
            _cellSize.y = tempX;
            _cellSize.x = _cellSize.y;
        }

        public override void ToPool()
        {
            base.ToPool();
            _cellSize = _unitTemplate.CellSize;
            _spriteRenderer.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
