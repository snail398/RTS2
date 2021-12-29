using System;
using Battle.Buildings;

namespace GameObjectView.Buildings
{
    public class CommandCenterView : BuildingView, IClickable
    {
        public event Action OnClick;
        
        public void Click()
        {
            OnClick?.Invoke();
        }

        public override BuildingType BuildingType => BuildingType.CommandCenter;
    }
}