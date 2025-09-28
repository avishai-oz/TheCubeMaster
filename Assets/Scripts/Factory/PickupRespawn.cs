using UnityEngine;
using Factory;
public class PickupRespawn : MonoBehaviour
{
    public IPickupFactory factory;
    public void Respawn()
    {
        if (factory != null) factory.Relocate(transform);
    }
}
