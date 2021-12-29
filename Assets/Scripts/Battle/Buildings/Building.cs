using System;
using Battle.Player;
using GameObjectView;
using UnityEngine;

namespace Battle.Buildings
{
    public abstract class Building : IBuildable, IDisposable, ISelectable
    {
        protected BuildingView View;
        private byte _PlayerId;
        private int _Width;
        private int _Height;

        public bool IsFriendly => _PlayerId == 0;

        public virtual void Initialize(BuildingView view, byte playerId, int width, int height)
        {
            View = view;
            _PlayerId = playerId;
            _Width = width;
            _Height = height;
            View.Initialize(playerId);
        }

        public virtual void Dispose()
        {
        }

        public virtual Transform Transform => View.transform;

        public virtual int Width => _Width;

        public virtual int Height => _Height;
    }
}