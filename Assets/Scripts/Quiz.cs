using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private List<QuestionData> _questions = new();

    [Header("Answers")]
    [SerializeField] private GameObject[] _answerButtons;
    
    [Header("Button Colors")]
    [SerializeField] private Sprite _defaultAnswerSprite;
    [SerializeField] private Sprite _correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Timer _timer;
    [SerializeField] private Image _timerImage;
    
    [Header("Scoring")]
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Progress Bar")]
    [SerializeField] private Slider _progressBar;
    
    public bool IsComplete { get; private set; }

    private int _correctAnswerIndex;
    private bool _hasAnswered = true;
    private QuestionData _currentQuestion;
    
    private Image[] _buttonImages;
    private Button[] _buttonComponents;
    private TextMeshProUGUI[] _buttonTexts;

    private void Awake()
    {
        _progressBar.maxValue = _questions.Count;
        _progressBar.value = 0;
    }

    private void Start()
    {
        CacheButtonComponents();
    }

    private void CacheButtonComponents()
    {
        _buttonImages = new Image[_answerButtons.Length];
        _buttonComponents = new Button[_answerButtons.Length];
        _buttonTexts = new TextMeshProUGUI[_answerButtons.Length];

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _buttonImages[i] = _answerButtons[i].GetComponent<Image>();
            _buttonComponents[i] = _answerButtons[i].GetComponent<Button>();
            _buttonTexts[i] = _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void LateUpdate()
    {
        _timerImage.fillAmount = _timer.FillFraction;
        if (_timer.LoadNextQuestion)
        {
            if (Mathf.Approximately(_progressBar.value, _progressBar.maxValue))
            {
                IsComplete = true;
                return;
            }

            _hasAnswered = false;
            GetNextQuestion();
            _timer.SetLoadAnsweringQuestion(false);
        }
        else if (!_hasAnswered && !_timer.IsAnsweringQuestion)
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
        _scoreText.text = "Score: " + _score.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == _currentQuestion.CorrectAnswerIndex)
        {
            _questionText.text = "Correct!";
            buttonImage = _buttonImages[index];
            buttonImage.sprite = _correctAnswerSprite;
            _score.IncrementCorrectAnswers();
        }
        else
        {
            _correctAnswerIndex = _currentQuestion.CorrectAnswerIndex;
            string correctAnswer = _currentQuestion.Answer(_correctAnswerIndex);
            _questionText.text = "Wrong!\n Correct answer is:\n" + correctAnswer;
            buttonImage = _buttonImages[_correctAnswerIndex];
            buttonImage.sprite = _correctAnswerSprite;
        }
    }

    private void GetNextQuestion()
    {
        if (_questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            _progressBar.value++;
            _score.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, _questions.Count);
        _currentQuestion = _questions[index];

        if (_questions.Contains(_currentQuestion))
        {
            _questions.Remove(_currentQuestion);
        }
    }

    private void DisplayQuestion()
    {
        _questionText.text = _currentQuestion.Question;

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _buttonTexts[i].text = _currentQuestion.Answer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _buttonComponents[i].interactable = state;
        }
    }

    private void SetDefaultButtonSprite()
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _buttonImages[i].sprite = _defaultAnswerSprite;
        }
    }
}