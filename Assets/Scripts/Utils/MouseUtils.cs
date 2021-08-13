using UnityEngine;

namespace Utils
{
    public static class MouseUtils
    {
        public static Vector3 GetMouseWorldPosititon(LayerMask mask)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 999f, mask))
            {
                return hit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}