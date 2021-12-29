using System.Collections.Generic;
using UnityEngine;

namespace Battle.Resources
{
    public class ResourceWallet
    {
        private readonly byte _OwnerId;
        private readonly Dictionary<ResourceType, int> _Resources = new Dictionary<ResourceType, int>();

        public ResourceWallet(byte ownerId)
        {
            _OwnerId = ownerId;
        }

        public int this[ResourceType resourceType]
        {
            get
            {
                if (!_Resources.TryGetValue(resourceType, out var currentAmount))
                {
                    Debug.LogError($"Dont have resource {resourceType} in wallet for player {_OwnerId}");
                }
                return currentAmount;
            }
        }

        public void AddResource(ResourceType resourceType, int amount)
        {
            _Resources.TryGetValue(resourceType, out var currentAmount);
            _Resources[resourceType] = currentAmount + amount;
        }

        public void RemoveResource(ResourceType resourceType, int amount)
        {
            if (!_Resources.TryGetValue(resourceType, out var currentAmount))
            {
                Debug.LogError($"Dont have resource {resourceType} in wallet for player {_OwnerId}");
                return;
            }

            if (currentAmount - amount <= 0)
            {
                _Resources[resourceType] = 0;
            }
            else
            {
                _Resources[resourceType] = currentAmount - amount;
            }
        }

        public bool EnoughResource(ResourceType resourceType, int amount)
        {
            if (!_Resources.TryGetValue(resourceType, out var currentAmount))
            {
                Debug.LogError($"Dont have resource {resourceType} in wallet for player {_OwnerId}");
                return false;
            }
            return currentAmount - amount >= 0;
        }
    }
}