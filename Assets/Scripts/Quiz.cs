using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;

    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    // Start is called before the first frame update
    void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; ++i)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
        SetButtonsSate(true);
    }

    void SetButtonsSate(bool IsEnable)
    {
        for (int i = 0; i < answerButtons.Length; ++i)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = IsEnable;
        }
    }

    void SetDefaultButtonsSprite()
    {
        for (int i = 0; i < answerButtons.Length; ++i)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        SetButtonsSate(false);
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
        }
        else
        {
            questionText.text = "Wrong Answer!. The correct answer is :\n" + question.GetAnswer(correctAnswerIndex);
        }
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }
}
