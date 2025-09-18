using UnityEngine;

public interface ICollectible
{
    // הפעולה שהאיסוף מבצע כשהשחקן נוגע בו
    void Collect(in CollectContext ctx);
}

public readonly struct CollectContext
{
    public CollectContext(GameObject player) { Player = player; }
    public GameObject Player { get; }
    // בהמשך נוכל להרחיב כאן: Score, PowerupManager, GamePresenter וכו'
}