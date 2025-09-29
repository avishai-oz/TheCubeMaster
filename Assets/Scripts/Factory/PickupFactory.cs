using UnityEngine;
using System.Collections.Generic;
using Enums;
using Factory;


[RequireComponent(typeof(BoxCollider))]
public class PickupFactory : MonoBehaviour , IPickupFactory
{
    [Header("Area")]
    public BoxCollider area;          
    public float minDistance = 1.5f;   

    [Header("Prefabs")]
    public GameObject coinPrefab;
    public GameObject speedPrefab;
    public GameObject jumpPrefab;
    public GameObject sizePrefab;

    [Header("Counts")]
    public int coinsCount = 10;
    public int speedCount = 2;
    public int jumpCount  = 2;
    public int sizeCount  = 2;

    public GameObject mesh;
    private readonly List<Vector3> _occupied = new List<Vector3>();

    
    public GameObject Create(PickupKind kind, Vector3 position, Transform parent = null)
    {
        GameObject kindPrefab = kind switch
        {
            PickupKind.Coin  => coinPrefab,
            PickupKind.Speed => speedPrefab,
            PickupKind.Jump  => jumpPrefab,
            PickupKind.Size  => sizePrefab,
            _ => null
        };
        if (kindPrefab == null) return null;

        var oldParent = parent ? parent : transform;
        var go = Instantiate(kindPrefab, position, Quaternion.identity, oldParent);

        var respawn = go.GetComponent<PickupRespawn>() ?? go.AddComponent<PickupRespawn>();
        respawn.factory = this;

        return go;
    }

    void Awake()
    {
        if (!area) area = GetComponent<BoxCollider>();
    }
    
    void Start()
    {
        SpawnInitial();
    }

    public Vector3 GetRandomPointInArea()
    {
        var center = area.bounds.center;
        var ext    = area.bounds.extents;
        float x = Random.Range(center.x - ext.x, center.x + ext.x);
        float y = Random.Range(center.y - ext.y, center.y + ext.y);
        float z = Random.Range(center.z - ext.z, center.z + ext.z);
        return new Vector3(x, y, z);
    }
    
    public bool TryPickFreePoint(out Vector3 point, int maxAttempts = 30)
    {
        float minSqr = minDistance * minDistance;

        for (int i = 0; i < maxAttempts; i++)
        {
            var p = GetRandomPointInArea();
            bool tooClose = false;

            foreach (var op in _occupied)
            {
                if ((p - op).sqrMagnitude < minSqr) { tooClose = true; break; }
            }

            if (!tooClose)
            {
                point = p;
                _occupied.Add(point);
                return true;
            }
        }

        point = GetRandomPointInArea();
        _occupied.Add(point);
        return true;
    }
    
    public bool Relocate(Transform item)
    {
        if (TryPickFreePoint(out var p))
        {
            item.position = p;
            var rb = item.GetComponent<Rigidbody>();
            if (rb) rb.linearVelocity = UnityEngine.Vector3.zero;
            return true;
        }
        return false;
    }
    public void SpawnInitial()
    {
        _occupied.Clear();

        SpawnManyByKind(PickupKind.Coin,  coinsCount);
        SpawnManyByKind(PickupKind.Speed, speedCount);
        SpawnManyByKind(PickupKind.Jump,  jumpCount);
        SpawnManyByKind(PickupKind.Size,  sizeCount);
    }
    
    void SpawnManyByKind(PickupKind kind, int count)
    {
        if (count <= 0) return;

        for (int i = 0; i < count; i++)
        {
            if (TryPickFreePoint(out var p))
                p.y = mesh.transform.localPosition.y + 1f;
                Create(kind, p, transform); 
        }
    }
   
}