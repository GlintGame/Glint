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

    private TextMeshProUGUI text;

    private float _currentTextDuration;
    private float _overallTextDuration;

    void Awake()
    {
        this.text = this.tutoCanvas.GetComponentInChildren<TextMeshProUGUI>();
        this.text.text = this.text.text.Replace("${KB}", InputsDictionnary.getSpriteIndex(keyboardParams));
        this.text.text = this.text.text.Replace("${GP}", InputsDictionnary.getSpriteIndex(gamepadParams));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ShowText());
        this.tutoCanvas.gameObject.SetActive(true);
    }

    private IEnumerator ShowText()
    {
        yield return new WaitWhile(
            () => !(InputManager.GetButton(this.keyboardParams.action)
                || InputManager.GetAxis(this.keyboardParams.action) > 0.9)
        );
        yield return new WaitForSeconds(1.0f);

        this.tutoCanvas.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
