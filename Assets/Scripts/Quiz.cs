using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    bool hasAnswered = false;

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
        else if (!timer.IsAnswering() && !hasAnswered)
        {
            DisplayAnswer(-1);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; ++i)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }

    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            hasAnswered = false;
            SetButtonsSate(true);
            SetDefaultButtonsSprite();
            GetRandomizeQuestion();
            DisplayQuestion();
        }
    }

    void GetRandomizeQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        questions.Remove(currentQuestion);
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
        DisplayAnswer(index);
    }

    private void DisplayAnswer(int index)
    {
        hasAnswered = true;
        SetButtonsSate(false);
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
        }
        else
        {
            questionText.text = "Sorry!. The correct answer is :\n" + currentQuestion.GetAnswer(correctAnswerIndex);
        }
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }
}
