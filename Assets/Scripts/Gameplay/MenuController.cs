using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    string gameSceneName = "GameScene"; // תעדכן לשם הסצנה שלך
    public GameObject gameStatus;

    void Start()
    {
        gameStatus.SetActive(false);
        Time.timeScale = 0f;          // עצור את המשחק עד שהשחקן ילחץ על "Play"
    }
    public void Play()
    {
        Time.timeScale = 1f;          // ליתר ביטחון אם עצרנו קודם
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        gameStatus.SetActive(true);
    }
}