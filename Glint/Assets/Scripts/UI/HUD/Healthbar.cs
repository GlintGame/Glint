using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject filledBar;

    private Sprite maskSprite;
    private SpriteMask maskComponent;
    private bool isDisplayed = false;

    private int maxHealth;

    void Awake()
    {
        this.maskComponent = this.GetComponent<SpriteMask>();
        this.maskSprite = this.maskComponent.sprite;
        this.maskComponent.sprite = null;
    }

    void Display(int totalHealth)
    {
        this.maxHealth = totalHealth;
        this.maskComponent.sprite = this.maskSprite;
        this.isDisplayed = true;
    }

    void Hide()
    {
        this.maskComponent.sprite = null;
        this.isDisplayed = false;
    }

    public void ChangeHealth(int remainingHealth, int totalHealth)
    {
        if (!isDisplayed && remainingHealth != totalHealth)
            Display(totalHealth);

        if (isDisplayed && remainingHealth == totalHealth)
            Hide();

        float proportion = (float)remainingHealth / (float)totalHealth;
        this.filledBar.transform.localScale = new Vector3(proportion, 1, 1);
    }
}
