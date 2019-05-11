using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryViewImage : MonoBehaviour
{
    public AspectRatioFitter AspectRationFitter;
    public Image Image;

    public void UpdateImage(Sprite sprite, float aspectRatio)
    {
        this.AspectRationFitter.aspectRatio = aspectRatio;
        this.Image.overrideSprite = sprite;
    }
}
