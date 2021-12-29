using System;
using UnityEngine;

namespace GameObjectView
{
    public class MineralView : GameObjectView, IClickable
    {       
        public event Action OnClick;

        [SerializeField] 
        private Vector2Int _GridPosition;
        [SerializeField] 
        private int _StartMineralAmount = 400;

        public Vector2Int GridPosition => _GridPosition;

        public int StartMineralAmount => _StartMineralAmount;

        public void OnValidate()
        {
            _GridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        }

        public void Click()
        {
            OnClick?.Invoke();
        }
    }
}