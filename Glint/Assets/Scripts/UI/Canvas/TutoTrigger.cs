using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger : MonoBehaviour
{    
    public Canvas tutoCanvas;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ShowText();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.HideText();
    }

    private void ShowText()
    {
        this.tutoCanvas.gameObject.SetActive(true);
    }

    private void HideText()
    {
        this.tutoCanvas.gameObject.SetActive(false);
    }
}
