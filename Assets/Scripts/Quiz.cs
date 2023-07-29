using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Button colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image imageTimer;
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }

    void Update()
    {
        imageTimer.fillAmount = timer.fillFraction;

        if (timer.isLoadNextQuestion)
        {
            GetNextQuestion();
            timer.isLoadNextQuestion = false;
        }
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; ++i)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }

    }

    void GetNextQuestion()
    {
        SetButtonsSate(true);
        SetDefaultButtonsSprite();
        DisplayQuestion();
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
        timer.CancelTimer();
        SetButtonsSate(false);
        DisplayAnswer(index);
    }

    private void DisplayAnswer(int index)
    {
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
