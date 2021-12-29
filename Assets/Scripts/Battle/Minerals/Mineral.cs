using System;
using Battle.Buildings;
using Battle.Player;
using GameObjectView;
using UnityEngine;

namespace Battle.Minerals
{
    public class Mineral : IBuildable, IDisposable, ISelectable
    {
        private readonly PlayerSelectService _PlayerSelectService;
        
        private int _MineralAmount;
        private MineralView _View;
        
        public Transform Transform => _View.transform;

        public int Width => 1;

        public int Height => 1;
        
        public Mineral(PlayerSelectService playerSelectService)
        {
            _PlayerSelectService = playerSelectService;
        }
        
        public void Initialize(int mineralAmount, MineralView view)
        {
            _MineralAmount = mineralAmount;
            _View = view;
            _View.OnClick += OnClick;
        }

        private void OnClick()
        {
            _PlayerSelectService.Select(this);
        }

        public void Dispose()
        {
            _View.OnClick -= OnClick;
        }
    }
}