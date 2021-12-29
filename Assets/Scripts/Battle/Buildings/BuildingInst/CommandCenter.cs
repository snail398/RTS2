using Battle.Player;
using GameObjectView;
using GameObjectView.Buildings;

namespace Battle.Buildings
{
    public class CommandCenter : Building
    {
        private readonly PlayerSelectService _PlayerSelectService;
        
        private CommandCenterView _CurrentView;

        public CommandCenter(PlayerSelectService playerSelectService)
        {
            _PlayerSelectService = playerSelectService;
        }

        public override void Initialize(BuildingView view, byte playerId, int width, int height)
        {
            base.Initialize(view, playerId, width, height);
            _CurrentView = (CommandCenterView) View;
            _CurrentView.OnClick += OnClick;
        }

        private void OnClick()
        {
            _PlayerSelectService.Select(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            _CurrentView.OnClick -= OnClick;
        }
    }
}