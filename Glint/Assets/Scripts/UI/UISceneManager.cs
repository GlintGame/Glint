using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour {

    private static UISceneManager instance;
    private GameObject UIPack;

    void Awake()
    {
        this.UIPack = GameObject.FindGameObjectWithTag("UIPack");

        if (UISceneManager.instance == null)
        {
            UISceneManager.instance = this;
        }

        TransitionCanvas.getInstance().FadeIn(0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(this.LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        TransitionCanvas.getInstance().FadeOut(0.5f);
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(0.5f));
        SceneManager.LoadScene(sceneName);
    }
}
