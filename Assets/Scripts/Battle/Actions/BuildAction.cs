namespace Battle.Actions
{
    public class BuildAction : ActionBase
    {
        public string BuildingName;
        public int X;
        public int Y;
        
        public override ActionType ActionType => ActionType.Build;

        public BuildAction(string buildingName, int x, int y)
        {
            BuildingName = buildingName;
            X = x;
            Y = y;
        }
    }
}