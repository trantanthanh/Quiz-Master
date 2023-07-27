using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter your question here";

    [SerializeField] string[] answer = new string[4];
    [SerializeField] int correctAnswerIndex = 0;

    public string GetQuestion()
    {
        return question;
    }
}
