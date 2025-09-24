using System;
using UnityEngine;

public class PlayerCollectContextProvider : MonoBehaviour, ICollectContextProvider
{
    public Action<int> OnAddScore;
    private PowerupManager _powerManager;

    void Awake()
    {
        _powerManager = GetComponent<PowerupManager>(); 
        if(GameManager.gameManager != null)
            OnAddScore = GameManager.gameManager.AddScore;
    }
    
    public CollectContext Build()
    {
        return new CollectContext(gameObject, _powerManager, OnAddScore);
    }

}
    