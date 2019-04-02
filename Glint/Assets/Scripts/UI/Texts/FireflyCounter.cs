using UnityEngine;

public class FireflyCounter : MonoBehaviour, ITextModifier
{
    public string TotalString;
    public string PickedString;

    public string ModifyText(string str)
    {
        str = str.Replace(TotalString, PlayerScore.MaxLuciole.ToString());
        return str.Replace(PickedString, PlayerScore.Luciole.ToString());
    }
}
