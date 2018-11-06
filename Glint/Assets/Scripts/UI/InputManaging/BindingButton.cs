using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Binding Button", fileName = "New Binding Button")]
public class BindingButton : ScriptableObject {
    public CustomInputType inputType = CustomInputType.KeyboardButton;
    public bool isNegative = false;
    public string action = "";
}
