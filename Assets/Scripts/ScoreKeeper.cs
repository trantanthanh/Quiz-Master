using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionSeen = 0;

    public void Init()
    {
        correctAnswers = 0;
        questionSeen = 0;
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void SetCorrectAns(int value)
    {
        correctAnswers = value;
    }

    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void SetQuestionSeen(int value)
    {
        questionSeen = value;
    }

    public void IncreaseCorrectAns()
    {
        ++correctAnswers;
    }

    public void IncreaseQuestionSeen()
    {
        ++questionSeen;
    }

    public int CalculateScore(int numOfQuestions)
    {
        if (numOfQuestions == 0)
        {
            return 0;
        }
        return Mathf.RoundToInt(correctAnswers / (float)numOfQuestions * 100);
    }
}
