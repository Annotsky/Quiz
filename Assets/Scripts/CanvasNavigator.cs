using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasNavigator : MonoBehaviour
{
    [SerializeField] private Quiz _quiz;
    [SerializeField] private EndScreen _endScreen;

    private void Start()
    {
        _quiz.gameObject.SetActive(true);
        _endScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        ShowFinalScreen();
    }

    private void ShowFinalScreen()
    {
        if (!_quiz.IsComplete) return;
        _quiz.gameObject.SetActive(false);
        _endScreen.gameObject.SetActive(true);
        _endScreen.ShowFinalScore();
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}