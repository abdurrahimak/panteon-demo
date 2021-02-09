using UnityEngine;

namespace Panteon.UI
{
    public class ProductionItem
    {
        public static ProductionItem Empty => new ProductionItem() { Name = "", Sprite = null, Object = null };

        public Sprite Sprite;
        public string Name;
        public ScriptableObject Object;
    }
}