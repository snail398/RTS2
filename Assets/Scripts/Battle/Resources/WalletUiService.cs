using System;
using System.Collections.Generic;
using UI;
using UI.Panels;
using UnityEngine;
using Zenject;

namespace Battle.Resources
{
    public class WalletUiService : IInitializable, IDisposable
    {
        private readonly UIService _UiService;
        private readonly ResourcesWalletService _ResourcesWalletService;

        private readonly Dictionary<byte, PlayerResourceWalletView> _PlayersWalletsMap = new Dictionary<byte, PlayerResourceWalletView>();

        public WalletUiService(UIService uiService, ResourcesWalletService resourcesWalletService)
        {
            _UiService = uiService;
            _ResourcesWalletService = resourcesWalletService;
        }

        void IInitializable.Initialize()
        {
            var walletViews = _UiService.GetPanel<WalletPanel>().GetViews<PlayerResourceWalletView>();
            foreach (var view in walletViews)
            {
                _PlayersWalletsMap.Add(view.PlayerId, view);
            }
            _ResourcesWalletService.OnResourceCountChanged += OnResourceCountChanged;
        }

        private void OnResourceCountChanged(byte playerId, ResourceType resourceType, int currentCount, int changeCount)
        {
            if (!_PlayersWalletsMap.TryGetValue(playerId, out var walletView))
            {
                Debug.LogError($"Dont have wallet for player {playerId}");
                return;
            }
            walletView.SetResourceAmount(resourceType, currentCount);
        }

        void IDisposable.Dispose()
        {
            _ResourcesWalletService.OnResourceCountChanged -= OnResourceCountChanged;
        }
    }

    [Serializable]
    public class PlayerResourceView
    {
        public ResourceType ResourceType;
        public ResourceView ResourceView;
    }
}