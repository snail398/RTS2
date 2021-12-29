using Zenject;

namespace Battle.Buildings
{
    public class CommandCenterFactory : BuildingFactoryBase<InternalCommandCenterFactory, CommandCenter>
    {
        public override BuildingType CreationBuildingType => BuildingType.CommandCenter;

        public CommandCenterFactory(InternalCommandCenterFactory internalFactory) : base(internalFactory) { }
    }

    public class InternalCommandCenterFactory :  PlaceholderFactory<CommandCenter> { }
}