using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    Quiz quiz;
    EndScreen endScreen;

    enum GameState
    {
        STATE_IN_GAME,
        STATE_ENDSCREEN
    };

    GameState _state = GameState.STATE_IN_GAME;

    void SetState(GameState state)
    {
        this._state = state;
        switch (this._state)
        {
            case GameState.STATE_IN_GAME:
                {
                    quiz.gameObject.SetActive(true);
                    endScreen.gameObject.SetActive(false);
                    break;
                }
            case GameState.STATE_ENDSCREEN:
                {
                    quiz.gameObject.SetActive(false);
                    endScreen.gameObject.SetActive(true);
                    endScreen.SetFinalScore();
                    break;
                }
        }
    }

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (this._state)
        {
            case GameState.STATE_IN_GAME:
                {
                    if (quiz.isComplete)
                    {
                        SetState(GameState.STATE_ENDSCREEN);
                    }
                    break;
                }
            case GameState.STATE_ENDSCREEN:
                {
                    break;
                }
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
