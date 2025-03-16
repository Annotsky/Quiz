using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasNavigator : MonoBehaviour
{
    [SerializeField] private Quiz _quiz;
    [SerializeField] private EndScreenResult endScreenResult;

    private void Start()
    {
        _quiz.gameObject.SetActive(true);
        endScreenResult.gameObject.SetActive(false);
    }

    private void Update()
    {
        ShowFinalScreen();
    }

    private void ShowFinalScreen()
    {
        if (!_quiz.IsComplete) return;
        _quiz.gameObject.SetActive(false);
        endScreenResult.gameObject.SetActive(true);
        endScreenResult.ShowFinalScore();
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