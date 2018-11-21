using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour, IActivable
{
    public Canvas text;

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
