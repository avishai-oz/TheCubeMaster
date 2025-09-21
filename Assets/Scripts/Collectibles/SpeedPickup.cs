// Assets/Scripts/Collectibles/SpeedPickup.cs
using UnityEngine;


    [RequireComponent(typeof(Collider))]
    public class SpeedPickup : MonoBehaviour, ICollectible
    {
        public float multiplier = 1.5f;
        public float durationSeconds = 12f;

        void Reset() => GetComponent<Collider>().isTrigger = true;

        public void Collect(in CollectContext playerContext)
        {
            if (playerContext.Power != null)
                playerContext.Power.Apply(PowerupTypes.PowerupType.Speed, multiplier, durationSeconds);
            else
                Debug.LogWarning("No PowerupManager on Player; SpeedPickup ignored.", this);

            Destroy(gameObject);
        }
    }

