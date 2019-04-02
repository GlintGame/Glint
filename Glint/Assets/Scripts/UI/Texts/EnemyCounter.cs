using UnityEngine;

public class EnemyCounter : MonoBehaviour, ITextModifier
{
    public string ReplacedString;
    
    public string ModifyText(string str)
    {
        return str.Replace(ReplacedString, PlayerScore.Kills.ToString());
    }
}
