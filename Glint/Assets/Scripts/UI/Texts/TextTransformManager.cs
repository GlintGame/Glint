using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTransformManager : MonoBehaviour {

    private static List<TextTransformManager> translatableList = new List<TextTransformManager>();
    private static List<TextTransformManager> spriteableList = new List<TextTransformManager>();

    private Translator translator;
    private InputSpriteGenerator spriteGenerator;

    private bool translatable;
    private bool spriteable;

    private string originalText;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        this.textMesh = this.GetComponent<TextMeshProUGUI>();
        this.originalText = this.textMesh.text;
        

        this.translator = this.GetComponent<Translator>();

        if (this.translator != null)
        {
            this.translatable = true;
            TextTransformManager.translatableList.Add(this);
        }


        this.spriteGenerator = this.GetComponent<InputSpriteGenerator>();

        if (this.spriteGenerator != null)
        {
            this.spriteable = true;
            TextTransformManager.spriteableList.Add(this);
        }
    }

    void Start()
    {
        this.UpdateText();
    }


    void UpdateText()
    {
        string text = this.originalText;

        if(this.spriteable)
        {
            text = this.spriteGenerator.ReplaceToSprite(text);
        }

        if(this.translatable)
        {
            text = this.translator.Translate(text);
        }

        this.textMesh.text = text;
    }
    

    public static void UpdateTranslatable()
    {
        TextTransformManager.translatableList.ForEach((translatableText) =>
        {
            translatableText.UpdateText();
        });
    }
    

    public static void UpdateSpriteable()
    {
        TextTransformManager.spriteableList.ForEach((spriteableText) =>
        {
            spriteableText.UpdateText();
        });
    }
}
