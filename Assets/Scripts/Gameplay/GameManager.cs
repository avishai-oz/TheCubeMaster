using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event System.Action<int,int> OnScoreChanged;
    public event System.Action OnWin;
    
    [Header("Game")] 
    public int targetScore = 50;
    public int Score {get; private set;}
    
    public static GameManager gameManager { get; private set; }

    
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        gameManager = this;
    }
    
    void Start()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score, targetScore);
    }
    
    public void AddScore(int amount) {
        Debug.Log($"[GM] AddScore({amount}) before={Score}", this);
        Score = Mathf.Max(0, Score + amount);
        OnScoreChanged?.Invoke(Score, targetScore);
        if (Score >= targetScore) {
            Debug.Log("[GM] Win!", this);
            OnWin?.Invoke();
        }
    }
    
}