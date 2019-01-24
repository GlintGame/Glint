using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour {

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LeaveMenu(string sceneName)
    {
        Debug.Log(sceneName);
        GameObject.Find("Canvas").GetComponent<PauseMenu>().Resume();
        LoadScene(sceneName);
    }
}
