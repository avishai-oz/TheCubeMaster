using Enums;

namespace Factory
{
    using UnityEngine;

    public interface IPickupFactory
    {
        GameObject Create(PickupKind kind, Vector3 position, Transform parent = null);
    }
}