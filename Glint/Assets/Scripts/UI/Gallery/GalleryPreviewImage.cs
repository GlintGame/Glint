using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class PreviewSelectEvent : UnityEvent<Sprite, float>
{
}

public class GalleryPreviewImage : MonoBehaviour, ISelectHandler
{
    public PreviewSelectEvent onSelect;

    public GameObject Image;
    private Sprite Sprite;
    private float AspectRatio;

    void Awake()
    {
        this.Sprite = this.Image.GetComponent<Image>().sprite;
        this.AspectRatio = this.Image.GetComponent<AspectRatioFitter>().aspectRatio;
    }

    public void OnSelect(BaseEventData eventData)
    {
        this.onSelect.Invoke(this.Sprite, this.AspectRatio);
    }
}
