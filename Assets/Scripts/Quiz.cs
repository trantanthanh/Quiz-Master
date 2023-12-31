using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quiz : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip[] rightSFX;
    [SerializeField] AudioClip wrongSFX;
    AudioSource soundPlayer;
    int currentRightSFXIdx = 0;

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    bool hasAnswered = false;
    public bool isComplete = false;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Button colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image imageTimer;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI progressText;
    int numOfQuestions = 0;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    void Awake()
    {
        isComplete = false;
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.Init();
        numOfQuestions = questions.Count;
        scoreKeeper.SetNumOfQuestions(numOfQuestions);
        progressBar.maxValue = numOfQuestions;
        progressBar.value = 0;
        soundPlayer = GetComponent<AudioSource>();
        currentRightSFXIdx = 0;
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

    void UpdateScoreText()
    {
        scoreText.text = "Score : " + scoreKeeper.CalculateScore() + "%";
        progressText.text = scoreKeeper.GetQuestionSeen() + "/" + numOfQuestions;
        progressBar.value = scoreKeeper.GetQuestionSeen();
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
            UpdateScoreText();
        }
        else
        {
            isComplete = true;
        }
    }

    void GetRandomizeQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        questions.Remove(currentQuestion);
        scoreKeeper.IncreaseQuestionSeen();
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
            scoreKeeper.IncreaseCorrectAns();
            soundPlayer.PlayOneShot(rightSFX[currentRightSFXIdx]);
            ++currentRightSFXIdx;
            if (currentRightSFXIdx > rightSFX.Length - 1)
            {
                currentRightSFXIdx = rightSFX.Length - 1;
            }
        }
        else
        {
            soundPlayer.PlayOneShot(wrongSFX);
            currentRightSFXIdx = 0;
            questionText.text = "Sorry!. The correct answer is :\n" + currentQuestion.GetAnswer(correctAnswerIndex);
        }
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
        UpdateScoreText();
    }
}
