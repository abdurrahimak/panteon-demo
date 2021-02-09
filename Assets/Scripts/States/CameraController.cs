using System;
using UnityEngine;
using Panteon.Extension;
using Panteon.Units;

namespace Panteon.Game
{
    public class CameraController
    {
        private Vector3 _pressWorldPosition;
        private Vector3 _pressMousePosition;

        public event Action SwitchStructureCreationState;

        public CameraController()
        {
        }

        public void Update()
        {
            Camera.main.orthographicSize += Time.deltaTime * 20f * Input.mouseScrollDelta.y * -1f;

            if (!ExtensionMethods.IsPointerOverUIElement())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _pressWorldPosition = ExtensionMethods.GetMouseWorldPosition();
                    _pressMousePosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    Camera.main.transform.position += (_pressWorldPosition - ExtensionMethods.GetMouseWorldPosition());
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    float distance = Vector3.Distance(_pressMousePosition, Input.mousePosition);
                    if (distance < 0.1f)
                    {
                        RaycastHit2D hitInfo = Physics2D.Raycast(ExtensionMethods.GetMouseWorldPosition(), Vector2.zero);

                        if (hitInfo.transform != null)
                        {
                            Unit unit = hitInfo.transform.GetComponent<Unit>();
                            unit.Select();
                        }
                        else
                        {
                            SwitchStructureCreationState?.Invoke();
                        }
                    }
                }
            }
        }
    }
}
