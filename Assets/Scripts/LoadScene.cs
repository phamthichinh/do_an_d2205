using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load()
    {
        string nameScene = PlayerPrefs.GetString("SceneName", "Level1");
        SceneManager.LoadScene(nameScene);
        Time.timeScale = 1f;
    }
}
