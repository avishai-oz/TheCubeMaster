// Assets/Scripts/Collectibles/SizePickup.cs
using UnityEngine;


    [RequireComponent(typeof(Collider))]
    public class SizePickup : MonoBehaviour, ICollectible
    {
        public float multiplier = 1.25f;
        public float durationSeconds = 12f;

        void Reset() => GetComponent<Collider>().isTrigger = true;

        public void Collect(in CollectContext ctx)
        {
            if (ctx.Power != null)
                ctx.Power.Apply(PowerupType.Size, multiplier, durationSeconds);
            else
                Debug.LogWarning("No PowerupManager on Player; SizePickup ignored.", this);

            Destroy(gameObject);
        }
    }
