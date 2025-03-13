using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "Question ")]
public class QuestionData : ScriptableObject
{
    [SerializeField, TextArea(2, 6)] private string _question;
    [SerializeField] private string[] _answers = new string[4];
    [SerializeField] private int _correctAnswerIndex;

    public string Question => _question;
    public string Answer(int index) => _answers[index];
    public int CorrectAnswerIndex => _correctAnswerIndex;
}