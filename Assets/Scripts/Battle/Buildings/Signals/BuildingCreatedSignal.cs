namespace Battle.Buildings
{
    public class BuildingCreatedSignal : ISignal
    {
        public Building Building;
        public byte PlayerId;

        public BuildingCreatedSignal(Building building, byte playerId)
        {
            Building = building;
            PlayerId = playerId;
        }
    }
}