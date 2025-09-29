using UnityEngine;


    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ICollectContextProvider))]
    public class PlayerCollision : MonoBehaviour
    {
        private ICollectContextProvider _provider;

        void Awake()
        {
            _provider = GetComponent<ICollectContextProvider>();
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"[PlayerCollision] entered: {other.name}", other);

            if (_provider == null) return;
            
            var playerContext = _provider.Build();
            bool found = false;


            foreach (var mb in other.GetComponents<MonoBehaviour>())
            {
                if (mb is ICollectible c)
                {
                    found = true;
                    Debug.Log($"[PlayerCollision] ICollectible found on: {other.name} → {mb.GetType().Name}", mb);
                    c.Collect(in playerContext);
                    break; // מספיק אחד
                }
            }
            if (!found)
                Debug.LogWarning($"[PlayerCollision] No ICollectible on {other.name}", other);
        }
    }