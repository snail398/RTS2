using System.Collections.Generic;
using System.Linq;
using UI;
using UI.Panels;
using UnityEngine;

public class UIService
{
    private readonly List<Panel> _Panels;
    private readonly UIState[] _UIStates;
    
    public UIService(PanelsContainer panelsContainer, UIPanelService lobbyUiPanelService)
    {
        _Panels = panelsContainer.Panels;
        _UIStates = lobbyUiPanelService.UIStates;
    }

    public void ChangeState<T>() where T : UIState
    {
        var state = _UIStates.OfType<T>().First();
        foreach (var panel in _Panels)
        {
            panel.gameObject.SetActive(state.Panels.Contains(panel.GetType()));
        }
    }

    public T GetPanel<T>()
    {
        var panel = _Panels.OfType<T>().First();
        if (panel == null)
            Debug.LogError($"Can't find panel {typeof(T)}");
        return panel;
    }
}
