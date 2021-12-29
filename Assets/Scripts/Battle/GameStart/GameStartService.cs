using System;
using Battle.Player;
using Zenject;

namespace Battle.GameStart
{
    public class GameStartService : IInitializable, IDisposable
    {
        private readonly SignalBus _SignalBus;
        private readonly PlayerService _PlayerService;

        public GameStartService(SignalBus signalBus, PlayerService playerService)
        {
            _SignalBus = signalBus;
            _PlayerService = playerService;
        }

        void IInitializable.Initialize()
        {
            _PlayerService.CreatePlayer(true);
            _PlayerService.CreatePlayer(false);
            _SignalBus.FireSignal(new GameStartedSignal());
        }

        void IDisposable.Dispose()
        {
            
        }
    }
}