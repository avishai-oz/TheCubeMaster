using UnityEngine;

public class CoinPickup : MonoBehaviour, ICollectible
{
    public int points = 1;

    void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void Collect(in CollectContext playerContext)
    {
        if (!playerContext.TryAddScore(points))
            Debug.LogWarning($"No score hook connected; coin +{points} not applied.", this);

        Destroy(gameObject);
    }
}