using UnityEngine;
using UnityEngine.UI;

public class MenuButton : Button
{
    new void Awake()
    {
        base.Awake();
        this.onClick.AddListener(onClickSound);
    }

    void onClickSound()
    {
        AudioManager.Play("Bip");
    }
}
