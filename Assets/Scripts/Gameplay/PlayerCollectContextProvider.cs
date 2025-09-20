using System;
using UnityEngine;

    public class PlayerCollectContextProvider : MonoBehaviour, ICollectContextProvider
    {
        public Action<int> AddScoreHook; // חבר ל-GameManager.AddScore או ל-Presenter
        private PowerupManager _power;

        void Awake() => _power = GetComponent<PowerupManager>();

        public CollectContext Build() => new CollectContext(gameObject, _power, AddScoreHook);
    }
    