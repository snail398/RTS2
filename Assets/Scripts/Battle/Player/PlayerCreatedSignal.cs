namespace Battle.Player
{
    public class PlayerCreatedSignal : ISignal
    {
        public readonly Player Player;

        public PlayerCreatedSignal(Player player)
        {
            Player = player;
        }
    }
}