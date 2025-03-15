using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeToCompleteQuestion;
    [SerializeField] private float _timeToShowCorrectAnswer;

    public bool LoadNextQuestion { get; private set; }
    public bool IsAnsweringQuestion { get; private set; }
    public float FillFraction { get; private set; }
    
    private float _timerValue;

    private void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        _timerValue = 0;
    }

    public void SetLoadAnsweringQuestion(bool value)
    {
        LoadNextQuestion = value;
    }

    private void UpdateTimer()
    {
        _timerValue -= Time.deltaTime;

        if (_timerValue > 0)
        {
            float totalTime = IsAnsweringQuestion ? _timeToCompleteQuestion : _timeToShowCorrectAnswer;
            FillFraction = _timerValue / totalTime;
        }
        else
        {
            IsAnsweringQuestion = !IsAnsweringQuestion;
            _timerValue = IsAnsweringQuestion ? _timeToCompleteQuestion : _timeToShowCorrectAnswer;

            if (IsAnsweringQuestion)
            {
                LoadNextQuestion = true;
            }
        }
    }
}