using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Player;
using GameObjectView;
using Settings;
using Zenject;

namespace Battle.Buildings
{
    public class PlayersBuildingsService : IInitializable, IDisposable
    {
        private readonly SignalBus _SignalBus;
        private readonly ResourceLoaderService _ResourceLoaderService;
        private readonly BuildService _BuildService;
        private readonly MapView _MapView;
        
        private Dictionary<byte, List<Building>> _PlayerBuildings = new Dictionary<byte, List<Building>>();

        public PlayersBuildingsService(SignalBus signalBus, ResourceLoaderService resourceLoaderService, BuildService buildService, MapView mapView)
        {
            _SignalBus = signalBus;
            _ResourceLoaderService = resourceLoaderService;
            _BuildService = buildService;
            _MapView = mapView;
        }

        void IInitializable.Initialize()
        {
            _SignalBus.Subscribe<PlayerCreatedSignal>(OnPlayerCreated, this);
            _SignalBus.Subscribe<BuildingCreatedSignal>(OnBuildingCreated, this);
        }

        private void OnPlayerCreated(PlayerCreatedSignal signal)
        {
            _PlayerBuildings.Add(signal.Player.Id, new List<Building>());
            var config = _ResourceLoaderService.LoadResource<BuildingsConfig>($"Settings/BuildingsConfig").Buildings[0];
            _BuildService.TryDirectBuild(config, _MapView.DefaultBases.Bases.FirstOrDefault(_ => _.PlayerId == signal.Player.Id).MineralZoneView.BaseGridPosition, signal.Player.Id);
        }

        private void OnBuildingCreated(BuildingCreatedSignal signal)
        {
            _PlayerBuildings[signal.PlayerId].Add(signal.Building);
        }

        void IDisposable.Dispose()
        {
            
        }

    }
}