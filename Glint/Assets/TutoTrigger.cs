using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using utils;
using TMPro;

public class TutoTrigger : MonoBehaviour {

    public Canvas tutoCanvas;
    public BindingButton keyboardParams;
    public BindingButton gamepadParams;

    [Range(0f, 5.0f)]
    public float displayDuration = 1.0f;

    private TextMeshProUGUI text;


    void Awake()
    {
        this.text = this.tutoCanvas.GetComponentInChildren<TextMeshProUGUI>();

        if (this.keyboardParams)
                this.text.text = this.text.text.Replace("${KB}", InputsDictionnary.getSpriteIndex(this.keyboardParams));

        if (this.gamepadParams)
            this.text.text = this.text.text.Replace("${GP}", InputsDictionnary.getSpriteIndex(this.gamepadParams));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(this.Wait());
        this.ShowText();
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
        Destroy(this.gameObject);
    }

}
