using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour, IActivable
{
    public Text text;

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Disactivate()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.text.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
