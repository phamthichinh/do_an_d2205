using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour 
{ /// Sét thời gian cho vòng game
    public float roundTime = 60f;
    private UIManager uiMan;

    private bool endingRound = false;

    private Board board;

    public int currentScore;
    public float displayScore;
    public float scoreSpeed;

    public int scoreTarget1, scoreTarget2, scoreTarget3;

    public int starCount;
    private void Awake()
    {
        starCount = PlayerPrefs.GetInt("STAR", 0);
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
    }
    private void Update()
    {
        if(roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            if(roundTime <= 0 )
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
        uiMan.timeText.text = roundTime.ToString("F0") + "s" ;
        displayScore = Mathf.Lerp(displayScore, currentScore, scoreSpeed * Time.deltaTime);
        uiMan.scoreText.text = displayScore.ToString("F0");
    }

    public void WinCheck()
    {
        
        uiMan.roundOverScreen.SetActive(true);

        uiMan.finalScoreText.text = "Your score: "  + currentScore.ToString();

        if(currentScore >= scoreTarget3)
        {
            uiMan.finishGame.text = "Congratulations! You earned 3 stars!";
            uiMan.winStars1.SetActive(true);
            uiMan.winStars2.SetActive(true);
            uiMan.winStars3.SetActive(true);
            uiMan.btn_stars.SetActive(true);
            starCount += 3;
        }
        else if (currentScore >= scoreTarget2 && currentScore < scoreTarget3)
        {
            uiMan.finishGame.text = "Congratulations! You earned 2 stars!";
            uiMan.winStars1.SetActive(true);
            uiMan.winStars2.SetActive(true);
            uiMan.btn_stars.SetActive(true);
            starCount += 2;
        }
        else if (currentScore >= scoreTarget1 && currentScore < scoreTarget2)
        {
            uiMan.finishGame.text = "Congratulations! You earned 1 star!";
            uiMan.winStars1.SetActive(true);
            uiMan.btn_stars.SetActive(true);
            starCount += 1;
        }
        else
        {
            uiMan.finishGame.text = "Oh no! No stars for you! Try again?";
            uiMan.btn_no_star.SetActive(true);
        }
        PlayerPrefs.SetInt("STAR", starCount);
        PlayerPrefs.Save();
        SFXManager.instance.PlayRoundOver();
    }

   

}
