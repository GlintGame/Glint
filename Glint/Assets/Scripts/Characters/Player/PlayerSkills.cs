using Interfaces.Player;
using System;
using System.Reflection;
using UnityEngine;

public class PlayerSkills : MonoBehaviour, ICharacterSkills
{
    private ISkill[] skills;

    // handling cooldowns 
    public bool CanAct
    {
        get
        {
            foreach(ISkill skill in this.skills)
            {
                if (!skill.PlayerCanAct())
                {
                    return false;
                }                
            }
            return true;
        }
    }

    private void Awake()
    {
        this.skills = this.GetComponents<ISkill>();
    }

    public void LaunchSkills(InputsParameters inputs)
    {
        foreach (ISkill skill in this.skills)
        {
            // get the input info as a string
            // getting the type, the prop of the typy and getting the value on the object
            Type inputType = inputs.GetType();
            PropertyInfo inputProp = inputType.GetProperty(skill.GetInputName());
            bool inputValue = (bool)inputProp.GetValue(inputs, null);

            if(inputValue && this.CanAct) // test inputs
            {
                this.StartCoroutine(skill.Launch());
            }
        }
    }
}
