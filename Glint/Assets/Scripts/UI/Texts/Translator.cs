using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour
{
    public string[] eng = new string[] { "1st text", "2nd text", "3rd text" };
    public string[] fr = new string[] { "1er texte", "2ème texte", "3ème texte" };

    public string Translate(string str)
    {

        int startChar = str.IndexOf("{");
        int endChar = str.IndexOf("}");
        int index = 0;

        while (startChar != -1
            && endChar != -1
            && index < eng.Length)
        {
            string subString = str.Substring(startChar, endChar - startChar + 1);

            if (UILanguage.language == SystemLanguage.French)
            {
                str = str.Replace(subString, fr[index]);
            }
            else
            {
                str = str.Replace(subString, eng[index]);
            }

            startChar = str.IndexOf("{");
            endChar = str.IndexOf("}");
            index++;
        }

        Debug.LogWarning(str);
        return str;
    }
}