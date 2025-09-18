using UnityEngine;

/*[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]*/
public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var behaviours = other.GetComponents<MonoBehaviour>();
        foreach (var mb in behaviours)
        {
            if (mb is ICollectible collectible)
            {
                collectible.Collect(new CollectContext(gameObject));
                break; // מספיק איסוף אחד
            }
        }
    }
}