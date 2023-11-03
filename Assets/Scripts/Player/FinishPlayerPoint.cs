using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlayerPoint : MonoBehaviour
{

    public Action OnPlayerTouched = null;

    private bool _isPlayerAlreadyTouched = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if(_isPlayerAlreadyTouched) return;
        _isPlayerAlreadyTouched = true;
        OnPlayerTouched?.Invoke();
        Destroy(gameObject);
        PersControl.setNormalMode = true;
        PersControl._alwaysRun = false;
    }

    private void Start()
    {
        var componentRenderer = transform.GetComponent<Renderer>();
        if (componentRenderer) componentRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        OnPlayerTouched = null;
    }
}
