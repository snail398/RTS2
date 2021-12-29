using Battle.Player;
using UnityEngine;

namespace GameObjectView
{
    public class SelectView : GameObjectView
    {
        public void SetSelect(ISelectable selectable)
        {
            if (selectable == null)
            {
                transform.position = new Vector3(-999, 0, 0);
                return;
            }
            var vector = new Vector3(selectable.Width, 0.01f, selectable.Height);
            transform.position = selectable.Transform.position + vector / 2;
            transform.localScale = vector;
        }
    }
}