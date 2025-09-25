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
        return Instantiate(kindPrefab, position, Quaternion.identity, parent);
    }

    void Awake()
    {
        if (!area) area = GetComponent<BoxCollider>();
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
}