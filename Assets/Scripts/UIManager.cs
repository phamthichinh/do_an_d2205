using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public RoundManager roundMan;
    public Text timeText;
    public Text scoreText;
    public Text finalScoreText;
    public Text finishGame;
    public GameObject winStars1, winStars2, winStars3;
    public GameObject roundOverScreen;
    public GameObject btn_no_star;
    public GameObject btn_stars;
    private Board theBoard;
    public GameObject pauseScreen;
    public GameObject settingScreen;
    public bool canShuffle = true;
    public int countShuffle ;
    public Text countShuffleText;
    public GameObject shuffleTime;
    public GameObject starToBuy;

    public Text starText;

    public Text notificationText;
    public GameObject notification;

    [SerializeField] Slider sliderVolume;
    public GameObject iconMute;
    public GameObject iconUnMute;

    private void Awake()
    {
        notification.SetActive(false);
        roundMan = FindObjectOfType<RoundManager>();
        theBoard = FindObjectOfType<Board>();
        countShuffle = 2;
        countShuffleText.text = countShuffle.ToString();
        Load();
    }
    private void Start()
    {
        starText.text = roundMan.starCount.ToString();
        btn_no_star.SetActive(false);
        btn_stars.SetActive(false);
        winStars1.SetActive(false);
        winStars2.SetActive(false);
        winStars3.SetActive(false);
    }

    private void Update()
    {
        if (countShuffle > 0)
        {
            shuffleTime.SetActive(true);
            starToBuy.SetActive(false);
        }
        if (countShuffle == 0)
        {
            shuffleTime.SetActive(false);
            starToBuy.SetActive(true);
        }
    }
    public void PauseUnPause()
    {
        if (!pauseScreen.activeInHierarchy)
        {   
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void ShuffleBoard()
    {
        CheckShuffle();
        
        if (canShuffle && theBoard.currentState == Board.BoardState.move)
        {
            theBoard.ShuffleBoard();
            countShuffle--;
        }
        else if(!canShuffle && roundMan.starCount >= 5 && theBoard.currentState == Board.BoardState.move)
        {
            BuyShuffleTime();
            notification.SetActive(true);
            notificationText.text = "You spent 5 stars in exchange for a board shuffle!";
            StartCoroutine(AfterDelay());
            
        }
        else if(!canShuffle && roundMan.starCount < 5 && theBoard.currentState == Board.BoardState.move)
        {
            notification.SetActive(true);
            notificationText.text = "You don't have enough stars to exchange. Let's collect more!";
            StartCoroutine(AfterDelay());
        }
        countShuffleText.text = countShuffle.ToString();
    }
    public void BuyShuffleTime()
    {
        
        
        theBoard.ShuffleBoard();
        roundMan.starCount -= 5;
        starText.text = roundMan.starCount.ToString();
        PlayerPrefs.SetInt("STAR", roundMan.starCount);
        PlayerPrefs.Save();
    }

    public IEnumerator AfterDelay()
    {
        yield return new WaitForSeconds(1f);
        notification.SetActive(false);
    }

    public void CheckShuffle()
    {
        if(countShuffle > 0 )
        {
            shuffleTime.SetActive(true);
            starToBuy.SetActive(false);
            canShuffle = true;
        }
        else if(countShuffle == 0)
        {
            shuffleTime.SetActive(false);
            starToBuy.SetActive(true);
            canShuffle = false;
        }
    }

    
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowSettingPanel()
    {
        if (!settingScreen.activeInHierarchy)
        {
            settingScreen.SetActive(true);
            Time.timeScale = 0f;
            
        }
        

    }
    public void HideSettingPanel()
    {
        if (settingScreen.activeInHierarchy)
        {
            settingScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ChangeVolume()
    {

        AudioListener.volume = sliderVolume.value;
        Save(); // Lưu giá trị volume
        if (sliderVolume.value == 0)
        {
            iconMute.SetActive(true);
            iconUnMute.SetActive(false);
        }
        else
        {
            iconMute.SetActive(false);
            iconUnMute.SetActive(true);
        }


    }

    public void Save()
    {
        PlayerPrefs.SetFloat("sliderkey", sliderVolume.value);
        PlayerPrefs.Save();

    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("sliderkey"))
        {
            sliderVolume.value = PlayerPrefs.GetFloat("sliderkey", 1f);
            AudioListener.volume = sliderVolume.value;
        }
        else
        {
            PlayerPrefs.SetFloat("sliderkey", 1);
            sliderVolume.value = PlayerPrefs.GetFloat("sliderkey", 1f);
            AudioListener.volume = sliderVolume.value;
        }

        if (sliderVolume.value == 0)
        {
            iconMute.SetActive(true);
            iconUnMute.SetActive(false);
        }
        else
        {
            iconMute.SetActive(false);
            iconUnMute.SetActive(true);
        }
        
    }

}
