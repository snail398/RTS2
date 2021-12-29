using Battle.Buildings;

namespace GameObjectView
{
    public abstract class BuildingView : GameObjectView
    {
        protected byte PlayerId;
        
        public abstract BuildingType BuildingType { get;}

        public virtual void Initialize(byte playerId)
        {
            PlayerId = playerId;
        }
    }
}