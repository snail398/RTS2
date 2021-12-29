using UI.Panels;

namespace UI
{
    public class BattleUIPanelService : UIPanelService
    {
        public override UIState[] UIStates { get; } =
        {
            new BuildingState(new[]
            {
                typeof(BuildingsPanel),
            }),
        };
    }
}