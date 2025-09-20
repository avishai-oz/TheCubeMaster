using System;
using UnityEngine;
public interface ICollectible
{
    void Collect(in CollectContext ctx);
}

/// "מכתב ליווי" לאיסופים – מה יש לשחקן להציע.
public readonly struct CollectContext
{
    public CollectContext(GameObject player, PowerupManager power, Action<int> addScore)
    {
        Player = player;
        Power = power;
        AddScore = addScore;
    }

    public GameObject Player { get; }
    public PowerupManager Power { get; }     // יכול להיות null
    public Action<int> AddScore { get; }     // יכול להיות null

    public bool TryAddScore(int points)
    {
        if (AddScore == null) return false;
        AddScore(points);
        return true;
    }
}