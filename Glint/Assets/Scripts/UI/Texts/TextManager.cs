using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour {

    private static List<TextManager> instances = new List<TextManager>();

    private ITextModifier[] textModifiers;

    private string originalText;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        this.textMesh = this.GetComponent<TextMeshProUGUI>();
        this.originalText = this.textMesh.text;

        this.textModifiers = this.GetComponents<ITextModifier>();
        TextManager.instances.Add(this);
    }

    void Start()
    {
        this.UpdateText();
    }


    void UpdateText()
    {
        string text = this.originalText;

        foreach(ITextModifier textModifier in this.textModifiers)
        {
            text = textModifier.ModifyText(text);
        }
        
        this.textMesh.text = text;
    }
    

    public static void UpdateAllText()
    {
        TextManager.instances.ForEach((textManager) =>
        {
            textManager.UpdateText();
        });
    }
}
