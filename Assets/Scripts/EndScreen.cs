using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    private Score _score;

    private void Awake()
    {
        _score = FindFirstObjectByType<Score>();
    }

    public void ShowFinalScore()
    {
        _finalScoreText.text = $"Congratulations!\nYou have: {_score.CalculateScore()}%\ncorrect answers!";
    }
}