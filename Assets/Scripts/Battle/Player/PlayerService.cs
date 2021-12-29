using System;
using System.Collections.Generic;
using Zenject;

namespace Battle.Player
{
    public class PlayerService : IInitializable, IDisposable
    {
        private readonly SignalBus _SignalBus;

        private readonly Dictionary<byte, Player> _Players = new Dictionary<byte, Player>();
        private byte _Counter;

        public PlayerService(SignalBus signalBus)
        {
            _SignalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _Counter = 0;
        }

        public void CreatePlayer(bool isPlayer)
        {
            var player = new Player
            {
                Id = _Counter++,
                IsPlayer = isPlayer,
            };
            _Players.Add(player.Id, player);
            _SignalBus.FireSignal(new PlayerCreatedSignal(player));
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}