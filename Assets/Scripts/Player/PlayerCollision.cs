using UnityEngine;


    /// מזהה כניסה ל-Trigger, שואל את ה-Provider על ההקשר, ומפעיל את האיסוף.
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ICollectContextProvider))]
    public class PlayerCollision : MonoBehaviour
    {
        private ICollectContextProvider _provider;

        void Awake() => _provider = GetComponent<ICollectContextProvider>();

        void OnTriggerEnter(Collider other)
        {
            if (_provider == null) return;
            var ctx = _provider.Build();

            // מוצאים כל קומפוננט שמממש ICollectible על האובייקט שפגענו בו
            foreach (var mb in other.GetComponents<MonoBehaviour>())
            {
                if (mb is ICollectible c)
                {
                    c.Collect(in ctx);
                    break; // מספיק אחד
                }
            }
        }
    }