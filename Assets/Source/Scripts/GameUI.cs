using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] public GameObject death;
    [SerializeField] public RewardButton adButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }
}
