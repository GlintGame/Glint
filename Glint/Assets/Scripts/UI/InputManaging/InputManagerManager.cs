using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InputManagerManager : MonoBehaviour
{
    private static SerializedObject _inputManagerSerObj;

    void Awake()
    {
        _inputManagerSerObj = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
    }

    public void Test()
    {
        Debug.Log(KeyCode.LeftShift.ToString());
    }

    public static void SetProperty(string axisName, string propertyName, string newStringValue)
    {
        GetChildProperty(GetAxisProperty(axisName), propertyName).stringValue = newStringValue;
        _inputManagerSerObj.ApplyModifiedProperties();
    }

    public static void SetProperty(string axisName, string propertyName, bool newBoolValue)
    {
        GetChildProperty(GetAxisProperty(axisName), propertyName).boolValue = newBoolValue;
        _inputManagerSerObj.ApplyModifiedProperties();
    }

    public static void SetProperty(string axisName, string propertyName, float newFloatValue)
    {
        GetChildProperty(GetAxisProperty(axisName), propertyName).floatValue = newFloatValue;
        _inputManagerSerObj.ApplyModifiedProperties();
    }

    private static SerializedProperty GetAxisProperty(string axisName)
    {
        SerializedObject serializedObject = _inputManagerSerObj;
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            if (axis.stringValue == axisName) return axis;
        }
        return null;
    }

    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }
    /*
    private static bool AxisDefined(string axisName)
    {
        SerializedObject serializedObject = _inputManagerSerObj;
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            if (axis.stringValue == axisName) return true;
        }
        return false;
    }

    public enum AxisType
    {
        KeyOrMouseButton = 0,
        MouseMovement = 1,
        JoystickAxis = 2
    };

    public class VirtualAxisList
    {
        public InputAxis[] axisList;
        public int Length {
            get
            {
                return axisList.Length;
            }
        }

        public void AddVirtualAxis(SerializedProperty axis)
        {
            InputAxis[] newAxisListe = new InputAxis[axisList.Length + 1];
            for (int i = 0; i < axisList.Length; i++)
            {
                newAxisListe[i] = axisList[i];
            }
            axisList = newAxisListe;
        }
    }

    public class InputAxis
    {
        public string name;
        public string descriptiveName;
        public string descriptiveNegativeName;
        public string negativeButton;
        public string positiveButton;
        public string altNegativeButton;
        public string altPositiveButton;

        public float gravity;
        public float dead;
        public float sensitivity;

        public bool snap = false;
        public bool invert = false;

        public AxisType type;

        public int axis;
        public int joyNum;

        public InputAxis(SerializedProperty axis)
        {
            this.name = GetChildProperty(axis, "m_Name").stringValue;
            this.descriptiveName = GetChildProperty(axis, "descriptiveName").stringValue;
            this.descriptiveNegativeName = GetChildProperty(axis, "descriptiveNegativeName").stringValue;
            this.negativeButton = GetChildProperty(axis, "negativeButton").stringValue;
            this.positiveButton = GetChildProperty(axis, "positiveButton").stringValue;
            this.altNegativeButton = GetChildProperty(axis, "altNegativeButton").stringValue;
            this.altNegativeButton = GetChildProperty(axis, "altPositiveButton").stringValue;

            this.gravity = GetChildProperty(axis, "gravity").floatValue;
            this.dead = GetChildProperty(axis, "dead").floatValue;
            this.sensitivity = GetChildProperty(axis, "sensitivity").floatValue;

            this.type = (AxisType)GetChildProperty(axis, "type").intValue;
            this.axis = GetChildProperty(axis, "axis").intValue;
            this.joyNum = GetChildProperty(axis, "joyNum").intValue;
        }
    }




    private static void AddAxis(InputAxis axis)
    {
        if (AxisDefined(axis.name)) return;

        SerializedObject serializedObject = _inputManagerSerObj;
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.arraySize++;
        serializedObject.ApplyModifiedProperties();

        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
        GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
        GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
        GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

        serializedObject.ApplyModifiedProperties();
    }



    private void LogAxes()
    {
        SerializedObject serializedObject = _inputManagerSerObj;
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            Debug.Log(axis.stringValue);
        }
    }

    private void LogChildProperty(SerializedProperty parent)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            Debug.Log(child.name);
        }
        while (child.Next(false));
    }*/
}

