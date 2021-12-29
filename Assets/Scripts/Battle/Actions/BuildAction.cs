namespace Battle.Actions
{
    public class BuildAction : ActionBase
    {
        public string BuildingName;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public byte PlayerId;
        
        public override ActionType ActionType => ActionType.Build;

        public BuildAction(string buildingName, int x, int y, int width, int height, byte playerId)
        {
            BuildingName = buildingName;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            PlayerId = playerId;
        }
    }
}