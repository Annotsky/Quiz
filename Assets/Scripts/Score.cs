using UnityEngine;

public class Score : MonoBehaviour
{
    private int _correctAnswers;
    private int _questionsSeen;

    public void IncrementCorrectAnswers()
    {
        _correctAnswers++;
    }

    public void IncrementQuestionsSeen()
    {
        _questionsSeen++;
    }

    public int CalculateScore()
    {
        return _questionsSeen == 0 ? 0 : Mathf.RoundToInt((float)_correctAnswers / _questionsSeen * 100);
    }
}