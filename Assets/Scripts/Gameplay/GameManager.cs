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
    public int targetScore = 50;
    public Transform player;
    public int Score {get; private set;}
    
    Rigidbody _rb;

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
        _rb = player ? player.GetComponent<Rigidbody>() : null;
        Score = 0;
        UpdateScoreUI();
    }

    void Update()
    {
        if (_rb)
        {
            float speed = _rb.velocity.magnitude;
            float height = player.position.y;
            speedText.text = $"Speed: {speed:0.0} m/s";
            hgtText.text = $"Height: {height:0.0} m";
        }
    }
    void UpdateScoreUI() {
        if (scoreText) scoreText.text = $"Score: {Score}/{targetScore}";
    }
    public void AddScore(int amount) {
        Score = Mathf.Max(0, Score + amount);
        UpdateScoreUI();
        if (Score >= targetScore) WinGame();
    }
    void WinGame()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}