using UnityEngine;
using TMPro;

public class GameUIPresenter : MonoBehaviour
    {
        [Header("UI")]
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI hgtText;
        public TextMeshProUGUI scoreText;
        public GameObject winPanel;
        
        public Transform player;

        void Awake()
        {
            
        }
        void Update()
        {
            updateStatsUI();
        }
        
        void OnEnable() {
            var gm = GameManager.gameManager;
            if (gm != null) {
                gm.OnScoreChanged += UpdateScoreUI;
                gm.OnWin += WinGame;
                UpdateScoreUI(gm.Score, gm.targetScore);
            }
        }

        void OnDisable() {
            var gm = GameManager.gameManager;
            if (gm != null) {
                gm.OnScoreChanged -= UpdateScoreUI;
                gm.OnWin -= WinGame;
            }
        }

        void updateStatsUI() {
            if (GameManager.gameManager != null && player != null)
            {
                var rb = player.GetComponent<Rigidbody>();
                if (rb)
                {
                    float speed = rb.velocity.magnitude;
                    float height = player.position.y;
                    if (speedText) speedText.text = $"Speed: {speed:0.0} m/s";
                    if (hgtText) hgtText.text = $"Height: {height:0.0} m";
                }
            }
        }
        
        void UpdateScoreUI(int Score, int targetScore) {
            if (scoreText) scoreText.text = $"Score: {Score}/{targetScore}";
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
