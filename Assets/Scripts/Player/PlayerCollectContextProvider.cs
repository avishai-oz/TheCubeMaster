using System;
using UnityEngine;

public class PlayerCollectContextProvider : MonoBehaviour, ICollectContextProvider
{
    public Action<int> AddScoreHook;
    private PowerupManager _power;

    void Awake()
    {
        _power = GetComponent<PowerupManager>();    
    }
    
    public CollectContext Build()
    {
        return new CollectContext(gameObject, _power, AddScoreHook);
    }

}
    