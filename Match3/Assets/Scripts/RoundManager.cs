using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

#region Fields

    public float roundTime = 60f;
    private UIManager uiMan;

    private bool endingRound = false;

    private Board board;

    public int currentScore;
    public float displayScore;
    public float scoreSpeed;
    
    #endregion

    #region Unity Events

    private void Awake()
    {
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
    }

    private void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;

            if (roundTime <= 0)
            {
                roundTime = 0;

                endingRound = true;
            }
        }

        if (endingRound && board.currentState == Board.BoardState.move)
        {
            WinCheck();
            endingRound = false;
        }

        uiMan.timeText.text = roundTime.ToString("0.0") + "s";

        displayScore = Mathf.Lerp(displayScore, currentScore, scoreSpeed * Time.deltaTime);
        uiMan.scoreText.text = displayScore.ToString("0");
    }

    #endregion

    #region Private Methods

    private void WinCheck()
    {
        uiMan.roundOverScreen.SetActive(true);
    }
    
    #endregion

    #region Public Methods

    #endregion

}
