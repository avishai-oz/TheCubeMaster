using UnityEngine;


    /// מזהה כניסה ל-Trigger, שואל את ה-Provider על ההקשר, ומפעיל את האיסוף.
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
            if (_provider == null) return;
            
            var playerContext = _provider.Build();

            foreach (var mb in other.GetComponents<MonoBehaviour>())
            {
                if (mb is ICollectible c)
                {
                    c.Collect(in playerContext);
                    break; // מספיק אחד
                }
            }
        }
    }