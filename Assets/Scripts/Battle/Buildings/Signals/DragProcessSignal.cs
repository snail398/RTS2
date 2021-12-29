using Settings;
using UnityEngine;

namespace Battle.Buildings
{
    public class DragProcessSignal : ISignal
    {
        public readonly BuildingConfig BuildingConfig;
        public readonly Vector3 MousePosition;

        public DragProcessSignal(BuildingConfig buildingConfig, Vector3 mousePosition)
        {
            BuildingConfig = buildingConfig;
            MousePosition = mousePosition;
        }
    }
}