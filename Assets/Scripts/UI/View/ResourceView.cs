using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceView : View
    {
        [SerializeField]
        private Text _ResourceAmount;

        public void SetResourceAmount(int amount)
        {
            _ResourceAmount.text = amount.ToString();
        }
    }
}