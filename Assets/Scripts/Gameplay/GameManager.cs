using UnityEngine;
using TMPro; 

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
        OnScoreChanged?.Invoke(targetScore, Score);
    }
    
    public void AddScore(int amount) {
        Score = Mathf.Max(0, Score + amount);
        OnScoreChanged?.Invoke(Score,  targetScore);  
        if (Score >= targetScore) {
            OnWin?.Invoke();
        }
    }
    
}