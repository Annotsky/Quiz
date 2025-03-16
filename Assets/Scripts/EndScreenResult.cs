using UnityEngine;
using TMPro;

public class EndScreenResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private Score _score;

    public void ShowFinalScore()
    {
        _finalScoreText.text = $"Congratulations!\nYou have: {_score.CalculateScore()}%\ncorrect answers!";
    }
}