using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour {

    public string[] eng = new string[] {"1st text","2nd text","3rd text"};
    public string[] fr = new string[] {"1er texte","2ème texte","3ème texte"};
    private TMP_Text textMesh;

    void Awake()
    {
        this.textMesh = this.gameObject.GetComponent<TMP_Text>();
        Debug.Log(this.textMesh);
    }
	// Use this for initialization
	void Start () {

        int startChar = this.textMesh.text.IndexOf("{");
        int endChar = this.textMesh.text.IndexOf("}");
        int index = 0;

		while(startChar != -1
            && endChar != -1
            && index < eng.Length)
        {
            string subString = this.textMesh.text.Substring(startChar, endChar - startChar + 1);

            if(UILanguage.language == SystemLanguage.French)
            {
                this.textMesh.text = this.textMesh.text.Replace(subString, fr[index]);
            }
            else
            {
                this.textMesh.text = this.textMesh.text.Replace(subString, eng[index]);
            }
            
            startChar = this.textMesh.text.IndexOf("{");
            endChar = this.textMesh.text.IndexOf("}");
            index++;
        }
	}
}
