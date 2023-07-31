using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    
    public void QuitGame()
    {
        string name = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SceneName", name);
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
