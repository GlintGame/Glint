using System.Collections;

namespace Interfaces.Player
{
    public interface ISkill
    {
        IEnumerator Launch();
        bool PlayerCanAct();
        string GetInputName();
    }
}
