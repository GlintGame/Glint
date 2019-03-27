using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GalleryPreviewImage : MonoBehaviour, ISelectHandler
{
    public GameObject previewImage;
    private Sprite previewSprite;
    private float previewAspectRation;

    private GameObject viewImage;
    private Image viewImageComponent;
    private AspectRatioFitter viewAspectRatioFitter;

    void Awake()
    {
        this.previewSprite = this.previewImage.GetComponent<Image>().sprite;
        this.previewAspectRation = this.previewImage.GetComponent<AspectRatioFitter>().aspectRatio;

        this.viewImage = GameObject.FindGameObjectWithTag("GalleryView");
        this.viewImageComponent = this.viewImage.GetComponent<Image>();
        this.viewAspectRatioFitter = this.viewImage.GetComponent<AspectRatioFitter>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.previewSprite);
        this.viewImageComponent.sprite = this.previewSprite;
        this.viewAspectRatioFitter.aspectRatio = this.previewAspectRation;
    }
}
