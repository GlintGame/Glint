using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleMenu : SelectableScreen
{
    public Button FocusButton;

    void Awake()    
    {
        SceneManager.sceneUnloaded += (Scene c) => { base.RemoveFromActiveScreens(this); };
        base.AddToActiveScreens(this);
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Desactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.gameObject);
        this.FocusButton.Select();
    }
}
