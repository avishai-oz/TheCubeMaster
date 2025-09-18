using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class CoinPickup : MonoBehaviour, ICollectible
{
    public int points = 1;

    void Reset()
    {
        // מבטיח שהמטבע יהיה Trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void Collect(in CollectContext ctx)
    {
        // בהמשך נחבר למערכת ניקוד; כרגע הדגמה:
        Debug.Log($"+{points} coin collected by {ctx.Player.name}");
        Destroy(gameObject);
    }
}