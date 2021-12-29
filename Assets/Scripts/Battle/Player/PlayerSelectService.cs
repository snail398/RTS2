using System;
using GameObjectView;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Battle.Player
{
    public class PlayerSelectService : IInitializable, IDisposable
    {
        private readonly ResourceLoaderService _ResourceLoaderService;
        private readonly SignalBus _SignalBus;
        
        private ISelectable _CurrentSelect;
        private SelectView _SelectView;

        public PlayerSelectService(ResourceLoaderService resourceLoaderService, SignalBus signalBus)
        {
            _ResourceLoaderService = resourceLoaderService;
            _SignalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            var prefab = _ResourceLoaderService.LoadResource<SelectView>("Prefabs/Select");
            _SelectView = Object.Instantiate(prefab, new Vector3(-999, 0, 0), Quaternion.identity);
            _SignalBus.Subscribe<EmptyClickSignal>(OnEmptyClick, this);
        }

        private void OnEmptyClick(EmptyClickSignal signal)
        {
            Select(null);
        }

        public void Select(ISelectable select)
        {
            _CurrentSelect = select;
            _SelectView.SetSelect(_CurrentSelect);
        }
        
        void IDisposable.Dispose()
        {
            _SignalBus.UnSubscribeFromAll(this);
        }
    }
}