namespace Battle.Buildings
{
    public interface IBuildingFactory 
    {
        BuildingType CreationBuildingType { get; }

        Building Create();
    }
}