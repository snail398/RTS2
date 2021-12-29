using System;
using System.Collections.Generic;
using Battle.Player;
using UnityEngine;
using Zenject;

namespace Battle.Resources
{
    public class ResourcesWalletService : IInitializable, IDisposable
    {
        //playerId, resourceType, currentCount, changeCount
        public event Action<byte, ResourceType, int, int> OnResourceCountChanged;
        
        private readonly SignalBus _SignalBus;

        private readonly Dictionary<byte, ResourceWallet> _PlayersResources = new Dictionary<byte, ResourceWallet>();

        public ResourcesWalletService(SignalBus signalBus)
        {
            _SignalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _SignalBus.Subscribe<PlayerCreatedSignal>(OnPlayerCreated, this);
        }

        private void OnPlayerCreated(PlayerCreatedSignal signal)
        {
            var wallet = new ResourceWallet(signal.Player.Id);
            _PlayersResources.Add(signal.Player.Id, wallet);
            AddResource(signal.Player.Id, ResourceType.Mineral, 228);
            AddResource(signal.Player.Id, ResourceType.Gas, 1488);
        }

        public void AddResource(byte playerId, ResourceType resourceType, int amount)
        {
            if (!_PlayersResources.TryGetValue(playerId, out var wallet))
            {
                Debug.LogError($"Dont have wallet for player {playerId}");
            }
            wallet.AddResource(resourceType, amount);
            OnResourceCountChanged?.Invoke(playerId, resourceType, wallet[resourceType], amount);
        }

        public void RemoveResource(byte playerId, ResourceType resourceType, int amount)
        {
            if (!_PlayersResources.TryGetValue(playerId, out var wallet))
            {
                Debug.LogError($"Dont have wallet for player {playerId}");
            }
            wallet.RemoveResource(resourceType, amount);
            OnResourceCountChanged?.Invoke(playerId, resourceType, wallet[resourceType], -amount);
        }


        public bool EnoughResource(byte playerId, ResourceType resourceType, int amount)
        {
            if (!_PlayersResources.TryGetValue(playerId, out var wallet))
            {
                Debug.LogError($"Dont have wallet for player {playerId}");
                return false;
            }
            return wallet.EnoughResource(resourceType, amount);
        }

        void IDisposable.Dispose()
        {
            _SignalBus.UnSubscribeFromAll(this); 
        }
    }
}