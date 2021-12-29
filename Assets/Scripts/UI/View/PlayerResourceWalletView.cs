using System.Collections.Generic;
using Battle.Resources;
using UnityEngine;

namespace UI
{
    public class PlayerResourceWalletView : View
    {
        [SerializeField] 
        private List<PlayerResourceView> _PlayersResourcesView;
        [SerializeField] 
        private byte _PlayerId;

        public byte PlayerId => _PlayerId;

        private readonly Dictionary<ResourceType, ResourceView> _PlayersResourcesViewsMap = new Dictionary<ResourceType, ResourceView>();

        private void Awake()
        {
            foreach (var resourceView in _PlayersResourcesView)
            {
                _PlayersResourcesViewsMap.Add(resourceView.ResourceType, resourceView.ResourceView);
            }   
        }

        public void SetResourceAmount(ResourceType resourceType, int amount)
        {
            if (!_PlayersResourcesViewsMap.TryGetValue(resourceType, out var view))
            {
                Debug.LogError($"Dont have resource view for {resourceType} for player {_PlayerId}");
                return;
            }
            view.SetResourceAmount(amount);
        }
    }
}