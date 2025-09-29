using UnityEngine;
using Factory;
public class PickupRespawn : MonoBehaviour
{
    public IPickupFactory factory;
    public void Respawn()
    {
        Debug.Log($"[Respawn] factory is null? {factory==null}", this);
        if (factory != null) factory.Relocate(transform);
    }
}
