using System;
using UnityEngine;

public class PlayerCollectContextProvider : MonoBehaviour, ICollectContextProvider
{
    public Action<int> OnAddScore;
    private PowerupManager _powerManager;

    void Awake()
    {
        _powerManager = GetComponent<PowerupManager>();
        TryBindScore();  
    }

    void OnEnable()  => TryBindScore(); 
    void Start()     => TryBindScore();

    void TryBindScore()
    {
        if (OnAddScore == null && GameManager.gameManager != null)
        {
            OnAddScore = GameManager.gameManager.AddScore;
            Debug.Log("[Provider] AddScoreHook connected ✅", this);
        }
    }

    public CollectContext Build()
    {
        return new CollectContext(gameObject, _powerManager, OnAddScore);
    }

}
    