using System;
using GameObjectView;
using UnityEngine;
using Zenject;

namespace Battle
{
    public class ClickService : IInitializable, IDisposable
    {
        private readonly Click _Click;
        private readonly DoubleClick _DoubleClick;
        private readonly LongClick _LongClick;
        private readonly SignalBus _SignalBus;

        public ClickService(Click click, DoubleClick doubleClick, LongClick longClick, SignalBus signalBus)
        {
            _Click = click;
            _DoubleClick = doubleClick;
            _LongClick = longClick;
            _SignalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _Click.OnClick += OnClick;
        }

        private void OnClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 999f, LayerMask.GetMask("Clickable")))
            {
                var clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
            }
            else
            {
                _SignalBus.FireSignal(new EmptyClickSignal());
            }
        }

        void IDisposable.Dispose()
        {
            _Click.OnClick -= OnClick;
        }
    }
    
    public class EmptyClickSignal : ISignal {}
}