﻿using System;
using UnityEngine;

public class LongClick : MonoBehaviour {
    public event Action OnLongClick;
    public float ClickDuration = 1;
    bool clicking = false;
    float totalDownTime = 0;
    private Vector3 clickPosition;
    
    // Update is called once per frame
    void Update()
    {
        // Detect the first click
        if (Input.GetMouseButtonDown(0)) {
            clickPosition = Input.mousePosition;
            totalDownTime = 0;
            clicking = true;
        }

        // If a first click detected, and still clicking,
        // measure the total click time, and fire an event
        // if we exceed the duration specified
        if (clicking && Input.GetMouseButton(0))
        {
            totalDownTime += Time.deltaTime;

            if (totalDownTime >= ClickDuration)
            {
                if ((Input.mousePosition - clickPosition).magnitude > 50) {
                    clicking = false;
                    return;
                }
                Debug.Log("Long click");
                clicking = false;
                OnLongClick?.Invoke();
            }
        }

        // If a first click detected, and we release before the
        // duraction, do nothing, just cancel the click
        if (clicking && Input.GetMouseButtonUp(0))
        {
            clicking = false;
        }
    }
}
