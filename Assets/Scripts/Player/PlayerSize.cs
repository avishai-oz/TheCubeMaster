using System;
using UnityEngine;



public class PlayerSize : MonoBehaviour
{
    private Vector3 _initialScale;
    private PowerupManager _powerupManager;

    void Awake()
    {
        _initialScale = transform.localScale;
        _powerupManager = GetComponent<PowerupManager>();
        if (_powerupManager == null)
            Debug.LogWarning("No PowerupManager found on Player; PlayerSize will not function.", this);
    }

    

    private void FixedUpdate()
    {
        if (_powerupManager != null)
        {
            float sizeMult = _powerupManager.SizeMult;
            transform.localScale = _initialScale * sizeMult;
        }
    }
}
