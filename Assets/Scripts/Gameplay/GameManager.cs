using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager { get; private set; }
    
    [Header("UI")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI hgtText;
    public TextMeshProUGUI scoreText;
    public GameObject winPanel;

    [Header("Game")] 
    public int targetScore = 1;
    public Transform player;
    public int _score {get; private set;}
    
    
    void Awake()
    {
        gameManager = this;
    }
    void Start()
    {
        _score = 0;
        scoreText.text = $"Score: {_score}/{targetScore}";

    }
    public void AddScore(int amount)
    {
        _score += amount;
        scoreText.text = $"Score: {_score}/{targetScore}";
        if (_score >= targetScore)
        {
            WinGame();
        }
    }
    void Update()
    {
        if (player != null)
        {
            float speed = player.GetComponent<Rigidbody>().velocity.magnitude;
            float height = player.position.y;
            speedText.text = $"Speed: {speed:0.0} m/s";
            hgtText.text = $"Height: {height:0.0} m";
        }
    }
    
    void WinGame()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}