using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using UnityEngine.UI;
using TMPro;

public class TutoTrigger : MonoBehaviour {

    public Canvas tutoCanvas;
    public BindingButton keyboardParams;
    public BindingButton gamepadParams;

    private TextMeshProUGUI text;
    private string keyboardInputText;
    private string gamepadInputText;
    private string gamepadInputType;

    private float _currentTextDuration;
    private float _overallTextDuration;

    void Awake()
    {
        this.text = this.tutoCanvas.GetComponentInChildren<TextMeshProUGUI>();
        getKeyboardInput();
        getGamepadInput();
        this.text.text = this.text.text.Replace("${KB}", keyboardInputText);
        this.text.text = this.text.text.Replace("${GP}", gamepadInputText);
    }

    private void getKeyboardInput()
    {
        InputBinding binding = getBinding(0, this.keyboardParams.action);
        string outputText = this.keyboardParams.isNegative ? binding.Negative.ToString() : binding.Positive.ToString();
        Debug.Log("text : " + outputText);
        Debug.Log("key : " + utils.InputsDictionnary.dictionnary[outputText]);
        this.keyboardInputText = utils.InputsDictionnary.dictionnary[outputText];
    }

    private void getGamepadInput()
    {
        InputBinding binding = getBinding(1, this.keyboardParams.action);

        string outputText;
        switch (this.gamepadParams.inputType)
        {
            case CustomInputType.GamepadButton:
            case CustomInputType.DigitalAxis:
                outputText = this.gamepadParams.isNegative ? binding.Negative.ToString() : binding.Positive.ToString();
                break;
            case CustomInputType.GamepadAxis:
                outputText = binding.Axis.ToString();
                break;
            default:
                outputText = "Error";
                break;
        }

        this.gamepadInputText = utils.InputsDictionnary.dictionnary[outputText];
    }

    private InputBinding getBinding(int index, string action)
    {
        return InputManager.GetAction(KeyBinder.controlScheme, action).Bindings[index];
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
