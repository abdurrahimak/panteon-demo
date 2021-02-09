using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panteon.Game
{
    public class AlphaChanger : MonoBehaviour
    {
        private List<SpriteRenderer> spriteRenderers;
        private Dictionary<SpriteRenderer, Color> spriteRendererColors;

        private void InitializeSpriteRenderers()
        {
            if (spriteRenderers == null)
            {
                spriteRendererColors = new Dictionary<SpriteRenderer, Color>();
                spriteRenderers = new List<SpriteRenderer>();
                foreach (var renderer in transform.GetComponentsInChildren<SpriteRenderer>(true))
                {
                    spriteRenderers.Add(renderer);
                    spriteRendererColors.Add(renderer, renderer.color);
                }
            }
        }

        public void SetAlpha(float alpha)
        {
            InitializeSpriteRenderers();
            foreach (var spriteRenderer in spriteRenderers)
            {
                Color color = spriteRenderer.color;
                color.a = alpha * spriteRendererColors[spriteRenderer].a;
                spriteRenderer.color = color;
            }
        }

        public void SetColor(Color color)
        {
            InitializeSpriteRenderers();
            foreach (var spriteRenderer in spriteRenderers)
            {
                color.a = spriteRenderer.color.a;
                spriteRenderer.color = color;
            }
        }

        public void SetDefault()
        {
            if (spriteRendererColors != null)
            {
                foreach (var rendererKvp in spriteRendererColors)
                {
                    rendererKvp.Key.color = rendererKvp.Value;
                }
            }
        }
    }
}
