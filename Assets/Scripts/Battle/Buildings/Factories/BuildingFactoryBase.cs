using Zenject;

namespace Battle.Buildings
{
    public abstract class BuildingFactoryBase<TInternalFactory, TBuilding> : IBuildingFactory where TInternalFactory : PlaceholderFactory<TBuilding> where TBuilding : Building 
    {
        private readonly TInternalFactory _InternalFactory;

        protected BuildingFactoryBase(TInternalFactory internalFactory)
        {
            _InternalFactory = internalFactory;
        }

        public abstract BuildingType CreationBuildingType { get; }

        public Building Create()
        {
            return _InternalFactory.Create();
        }
    }
}