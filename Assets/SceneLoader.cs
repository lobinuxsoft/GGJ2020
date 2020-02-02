using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float delayChangeScene = 1.5f;
    public static void StaticCallLoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName, delayChangeScene));
    }

    IEnumerator ChangeScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public static void StaticCallQuit() 
    {
        Application.Quit();
    }
}
