using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeToCompleteQuestion;
    [SerializeField] private float _timeToShowCorrectAnswer;

    //что-то сделать с публичными полями, используются тут и в Quiz
    public bool LoadNextQuestion;
    public float FillFraction;
    public bool IsAnsweringQuestion;

    private float _timerValue;

    private void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        _timerValue = 0;
    }

    private void UpdateTimer()
    {//повторяющийся код
        _timerValue -= Time.deltaTime;

        if (IsAnsweringQuestion)
        {
            if (_timerValue > 0)
            {
                FillFraction = _timerValue / _timeToCompleteQuestion;
            }
            else
            {
                IsAnsweringQuestion = false;
                _timerValue = _timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (_timerValue > 0)
            {
                FillFraction = _timerValue / _timeToShowCorrectAnswer;
            }
            else
            {
                IsAnsweringQuestion = true;
                _timerValue = _timeToCompleteQuestion;
                LoadNextQuestion = true;
            }
        }
    }
}