using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : Button
{
    override public void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        AudioManager.Play("Bip");
    }
}
