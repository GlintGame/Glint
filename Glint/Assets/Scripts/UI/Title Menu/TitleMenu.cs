using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleMenu : SelectableScreen
{
    void Awake()    
    {
        this.Activate();
    }

    public override void Activate()
    {
        base.AddToActiveScreens(this);
    }

    public override void Desactivate()
    {
        base.RemoveFromActiveScreens(this);
    }

    public override void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.gameObject);
        this.focusButton.Select();
    }
}
