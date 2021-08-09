using System.Collections.Generic;
using System.Linq;
using UI.Panels;
using UnityEngine;

namespace UI
{
    public class PanelsContainer : MonoBehaviour
    {
        [SerializeField]
        private List<Panel> _Panels = new List<Panel>();

        public List<Panel> Panels => _Panels;

        private void OnValidate()
        {
            _Panels = GetComponentsInChildren<Panel>(true).ToList();
        }
    }
}