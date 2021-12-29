﻿using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragExitHandler : View, IPointerExitHandler {
    public event Action<PointerEventData> OnExitBuildingArea;
    
    public void OnPointerExit(PointerEventData eventData) {
        Vector2 mousePos = Input.mousePosition;
        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform) transform, mousePos))
            OnExitBuildingArea?.Invoke(eventData);
    }
}
