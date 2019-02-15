using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using utils;
using TMPro;

public class TutoTrigger : MonoBehaviour
{

    public static List<TutoTrigger> instances = new List<TutoTrigger>();

    public Canvas tutoCanvas;
    public BindingButton keyboardParams;
    public BindingButton gamepadParams;

    private string keyboardString = "$/KB/";
    private string gamepadString = "$/GP/";

    private string originalString;

    [Range(0f, 5.0f)]
    public float displayDuration = 1.0f;

    private TextMeshProUGUI textMesh;

    void Awake()
    {
        TutoTrigger.instances.Add(this);

        this.textMesh = this.tutoCanvas.GetComponentInChildren<TextMeshProUGUI>();
        originalString = this.textMesh.text;

        UpdateReplace();
    }

    public static void UpdateAll()
    {
        TutoTrigger.instances.ForEach(
            (instance) => instance.UpdateReplace()
        );
    }

    private void UpdateReplace()
    {
        string text = this.originalString;

        if (this.keyboardParams)
            text = text.Replace(this.keyboardString, InputsDictionnary.getSpriteIndex(this.keyboardParams));

        if (this.gamepadParams)
            text = text.Replace(this.gamepadString, InputsDictionnary.getSpriteIndex(this.gamepadParams));


        this.textMesh.text = text;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ShowText();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.HideText();
    }




    private IEnumerator Wait()
    {
        if (this.keyboardParams || this.gamepadParams)
        {
            yield return new WaitWhile(
                () => !(InputManager.GetButton(this.keyboardParams.action)
                    || InputManager.GetAxis(this.keyboardParams.action) > 0.9)
            );
        }

        yield return new WaitForSeconds(displayDuration);


        this.HideText();
    }

    private void ShowText()
    {
        this.tutoCanvas.gameObject.SetActive(true);
    }

    private void HideText()
    {
        this.tutoCanvas.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

}
