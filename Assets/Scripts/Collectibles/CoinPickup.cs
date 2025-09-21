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
        Debug.Log($"+{points} coin collected by {playerContext.Player.name}");
        Destroy(gameObject);
    }
}