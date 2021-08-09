using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public abstract class ViewsContainer : View
    {
        [SerializeField]
        private List<View> _Views;

        private void OnValidate()
        {
            _Views = GetComponentsInChildren<View>(true).ToList();
        }

        public T GetView<T>()
        {
            var view = _Views.OfType<T>().First();
            if (view == null)
                Debug.LogError($"Can't find view {typeof(T)} in views container {GetType()}");
            return view;
        }
    }
}