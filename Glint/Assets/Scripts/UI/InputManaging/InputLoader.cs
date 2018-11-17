using System.IO;
using System.Text;
using UnityEngine;
using Luminosity.IO;

public static class InputLoader {

    public static void PlayerPrefsSave(string location)
    {
        StringBuilder output = new StringBuilder();
        InputSaverXML saver = new InputSaverXML(output);
        InputManager.Save(saver);

        PlayerPrefs.SetString(location, output.ToString());
    }

    public static void PlayerPrefsLoad(string location)
    {
        if (PlayerPrefs.HasKey(location))
        {
            string xml = PlayerPrefs.GetString(location);
            using (TextReader reader = new StringReader(xml))
            {
                InputLoaderXML loader = new InputLoaderXML(reader);
                InputManager.Load(loader);
            }
        }
    }

    public static void PlayerPrefsDelete(string location)
    {
        if (PlayerPrefs.HasKey(location))
        {
            PlayerPrefs.DeleteKey(location);
        }
    }
}
