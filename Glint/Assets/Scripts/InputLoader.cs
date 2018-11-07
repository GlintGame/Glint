using System.IO;
using System.Text;
using UnityEngine;
using Luminosity.IO;

public static class InputLoader {

    public static void PlayerPrefsSave()
    {
        StringBuilder output = new StringBuilder();
        InputSaverXML saver = new InputSaverXML(output);
        InputManager.Save(saver);

        PlayerPrefs.SetString("MyGame.InputConfig", output.ToString());
    }

    public static void PlayerPrefsLoad()
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

    public static void PlayerPrefsDelete()
    {
        if (PlayerPrefs.HasKey("MyGame.InputConfig"))
        {
            PlayerPrefs.DeleteKey("MyGame.InputConfig");
        }
    }
}
