using System.Collections.Generic;
using UnityEngine;

namespace GameObjectView
{
    public class MineralZoneView : GameObjectView
    {
        [SerializeField] 
        private Vector2Int _BaseGridPosition;
        [SerializeField] 
        private List<MineralView> _MineralsViews;

        public Vector2Int BaseGridPosition => _BaseGridPosition;

        public List<MineralView> MineralsViews => _MineralsViews;

        private void OnValidate()
        {
            var basePositionView = GetComponentInChildren<BasePlaceView>();
            _BaseGridPosition = new Vector2Int((int)basePositionView.transform.position.x, (int)basePositionView.transform.position.z);
            var mineralsViews = GetComponentsInChildren<MineralView>();
            _MineralsViews = new List<MineralView>();
            foreach (var mineralsView in mineralsViews)
            {
                mineralsView.OnValidate();
                _MineralsViews.Add(mineralsView);
            }
        }
    }
}