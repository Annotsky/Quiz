using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionData> questions = new();

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    private int _correctAnswerIndex;
    private bool _hasAnswered = true;

    [Header("Button Colors")]
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    private Timer _timer;

    [Header("Scoring")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private Score _score;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    private QuestionData _currentQuestion;
    public bool isComplete;

    private void Awake()
    {
        _timer = FindFirstObjectByType<Timer>();
        _score = FindFirstObjectByType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void LateUpdate() //���� ��� ��������
    {
        timerImage.fillAmount = _timer.fillFraction;
        if (_timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            _hasAnswered = false;
            GetNextQuestion();
            _timer.loadNextQuestion = false;
        }
        else if (!_hasAnswered && !_timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        _hasAnswered = true;
        DisplayAnswer(index);
        SetButtonState(false);
        _timer.CancelTimer();
        scoreText.text = "����: " + _score.CalculateScore() + "%";        
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == _currentQuestion.CorrectAnswerIndex)
        {
            questionText.text = "���������!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            _score.IncrementCorrectAnswers();
        }
        else
        {
            _correctAnswerIndex = _currentQuestion.CorrectAnswerIndex;
            string correctAnswer = _currentQuestion.Answer(_correctAnswerIndex);
            questionText.text = "�����������!\n ���������� �����:\n" + correctAnswer;
            buttonImage = answerButtons[_correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            _score.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        _currentQuestion = questions[index];

        if (questions.Contains(_currentQuestion))
        {
            questions.Remove(_currentQuestion);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = _currentQuestion.Question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _currentQuestion.Answer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprite()
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}