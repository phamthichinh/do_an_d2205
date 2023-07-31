using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public float delaySecond = 1f;
    public RoundManager roundMan;
    private void Start()
    {
        roundMan = FindObjectOfType<RoundManager>();
    }
    public void ModelSelect()
    {
        StartCoroutine(LoadAfterDelay());
        roundMan.currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", roundMan.currentLevel);
        PlayerPrefs.Save();
        string name = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SceneName", name);
    }
    IEnumerator LoadAfterDelay()
    {
       
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
