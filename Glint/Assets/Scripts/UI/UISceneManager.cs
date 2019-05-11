using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour {

    public delegate void BeforeUnloadDelegate();

    void Awake()
    {
        TransitionCanvas.getInstance().FadeIn(0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "Tuto")
            AudioManager.Play("Strong");

        StartCoroutine(this.LoadSceneCoroutine(sceneName, () => { }));
    }

    public void LoadScene(string sceneName, BeforeUnloadDelegate beforeUnloadDelegate)
    {
        StartCoroutine(this.LoadSceneCoroutine(sceneName, beforeUnloadDelegate));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, BeforeUnloadDelegate beforeUnloadDelegate)
    {
        TransitionCanvas.getInstance().FadeOut(0.5f);
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(0.5f));
        beforeUnloadDelegate();
        PlayerScore.Reset();
        SceneManager.LoadScene(sceneName);
    }
}
