using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Luminosity.IO;

public class ButtonParam : MonoBehaviour {

    public BindingButton buttonParams;
    GameObject inputSetting;
    KeyBinder keyBinder;
    Button nativeButtonScript;
    TextMeshProUGUI buttonText;

    void Awake()
    {
        this.inputSetting = GameObject.FindGameObjectWithTag("SettingsCanvas");
        this.keyBinder = this.inputSetting.GetComponentInChildren<KeyBinder>();
        this.nativeButtonScript = this.GetComponent<Button>();
    }

    void OnEnable()
    {
        this.buttonText = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void KeyBind()
    {
        this.keyBinder.KeyBind(this.buttonParams, gameObject);
        nativeButtonScript.interactable = false;
        buttonText.text = "...";
    }

    public void UpdateButton()
    {
        this.buttonText.text = "<sprite=" + utils.InputsDictionnary.getSpriteIndex(this.buttonParams) + ">";
    }
}
