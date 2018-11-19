using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;

	void Update () {
		if(InputManager.GetButtonDown("UI_Menu"))
        {
            if(PauseMenu.gameIsPaused)
            {
                this.Resume();
            }
            else
            {
                this.Pause();
            }
        }
	}


    void Pause()
    {
        this.PauseMenuUI.SetActive(true);
        PauseMenu.gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        this.PauseMenuUI.SetActive(false);
        PauseMenu.gameIsPaused = false;
        Time.timeScale = 1f;
    }
}
