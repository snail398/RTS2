using System.Collections.Generic;

namespace GameObjectView
{
    public class MapView : GameObjectView
    {
        public DefaultBases DefaultBases;
        public List<MineralZoneView> MineralZones;
        public ImpassableCells ImpassibleCells;
    }
}