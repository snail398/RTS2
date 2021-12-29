﻿using System;
using UnityEngine;

public class Click : MonoBehaviour {
    
    public event Action OnClick;
    public event Action OnRelease;
    
    bool clicking = false;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            clicking = true;
            OnClick?.Invoke();
        }
        if (clicking && Input.GetMouseButtonUp(0))
        {
            OnRelease?.Invoke();
            clicking = false;
        }
    }

}
