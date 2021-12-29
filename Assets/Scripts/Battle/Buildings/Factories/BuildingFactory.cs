using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Buildings
{
    public class BuildingFactory
    {
        private readonly Dictionary<BuildingType, IBuildingFactory> _FactoriesMap;

        public BuildingFactory(List<IBuildingFactory> factories)
        {
            _FactoriesMap = factories.ToDictionary(_ => _.CreationBuildingType, _ => _);
        }

        public Building CreateBuilding(BuildingType buildingType)
        {
            if (!_FactoriesMap.TryGetValue(buildingType, out var factory))
            {
                Debug.LogError($"Dont have factory fro type {buildingType}");
            }

            return factory.Create();
        }
    }
}