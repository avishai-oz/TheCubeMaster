// Assets/Scripts/Collectibles/JumpPickup.cs
using UnityEngine;


    [RequireComponent(typeof(Collider))]
    public class JumpPickup : MonoBehaviour, ICollectible
    {
        public float multiplier = 1.5f;
        public float durationSeconds = 12f;

        void Reset() => GetComponent<Collider>().isTrigger = true;

        public void Collect(in CollectContext ctx)
        {
            if (ctx.Power != null)
                ctx.Power.Apply(PowerupTypes.PowerupType.Jump, multiplier, durationSeconds);
            else
                Debug.LogWarning("No PowerupManager on Player; JumpPickup ignored.", this);

            Destroy(gameObject);
        }
    }
