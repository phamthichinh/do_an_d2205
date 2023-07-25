using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public float delaySecond = 1f;

    public void ModelSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
