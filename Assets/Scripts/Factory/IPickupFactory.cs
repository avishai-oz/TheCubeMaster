using Enums;

namespace Factory
{
    using UnityEngine;

    public interface IPickupFactory
    {
        void SpawnInitial();
        bool TryPickFreePoint(out UnityEngine.Vector3 point, int maxAttempts = 30);
        bool Relocate(UnityEngine.Transform item);
        GameObject Create(PickupKind kind, Vector3 position, Transform parent = null);
    }
}