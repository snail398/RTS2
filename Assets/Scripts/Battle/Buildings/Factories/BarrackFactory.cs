using Zenject;

namespace Battle.Buildings
{
    public class BarrackFactory : BuildingFactoryBase<InternalBarrackFactory, Barrack>
    {
        public override BuildingType CreationBuildingType => BuildingType.Barrack;

        public BarrackFactory(InternalBarrackFactory internalFactory) : base(internalFactory) { }
    }
    
    public class InternalBarrackFactory : PlaceholderFactory<Barrack> { }
}