using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using Luminosity.IO;

public class KeyBinder : MonoBehaviour {

    public int timeout = 10;
    public string controlScheme = "Player";
    public UnityEngine.UI.Button btn;

    // Use this for initialization
    void Start () {
        // Load settings to PlayerPrefs
        PlayerPrefsLoad();
        btn.Select();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void LogLateral ()
    {
        Debug.Log(InputManager.GetAction("Player", "lateral").Bindings[0].Positive);
    }

    public void KeyBind(string buttonParam)
    {
        string[] tableParam = buttonParam.Split(' ');

        string action = tableParam[0];

        bool negative = tableParam[1] == "negative" ? true : false;

        Debug.LogFormat("action : {0}, is negative : {1}", action, negative);
        Debug.Log(buttonParam);

        //KeyboardKeyBinding(action, negative);
    }

    public void KeyboardKeyBinding (string action, bool negative)
    {
        ScanSettings settings = new ScanSettings
        {
            ScanFlags = ScanFlags.Key,

            CancelScanKey = KeyCode.Escape,

            Timeout = timeout
        };
        

        InputManager.StartInputScan(settings, result =>
        {
            InputAction inputAction = InputManager.GetAction(controlScheme, action);
            if (!negative)
            {
                inputAction.Bindings[0].Positive = result.Key;
            }
            else
            {
                inputAction.Bindings[0].Negative = result.Key;
            }

            // Save the settings to PlayerPrefs
            PlayerPrefsSave();
            return true;
        });
    }

    private void PlayerPrefsSave()
    {
        StringBuilder output = new StringBuilder();
        InputSaverXML saver = new InputSaverXML(output);
        InputManager.Save(saver);

        PlayerPrefs.SetString("MyGame.InputConfig", output.ToString());
    }

    private void PlayerPrefsLoad()
    {
        if (PlayerPrefs.HasKey("MyGame.InputConfig"))
        {
            string xml = PlayerPrefs.GetString("MyGame.InputConfig");
            using (TextReader reader = new StringReader(xml))
            {
                InputLoaderXML loader = new InputLoaderXML(reader);
                InputManager.Load(loader);
            }
        }
    }

}
