using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ProgressJudge))]
public class ProgressJudgeDrawer : PropertyDrawer
{
    string UseProgressSwitch = "UseProgressSwitch";
    string startSwitch = "startSwitch";
    string endSwitch = "endSwitch";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // UseProgressSwitchプロパティの表示
        SerializedProperty UseProgressSwitchProp = property.FindPropertyRelative(UseProgressSwitch);
        Rect useProgressSwitchRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(useProgressSwitchRect, UseProgressSwitchProp, new GUIContent("Use Progress Switch"));

        if (UseProgressSwitchProp.boolValue)
        {
            // インデントレベルを増やす
            EditorGUI.indentLevel++;

            // startSwitchプロパティの表示
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            PresentProperty(startSwitch, ref position, property);

            // endSwitchプロパティの表示
            PresentProperty(endSwitch, ref position, property);

            // インデントレベルを元に戻す
            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private void PresentProperty(string name, ref Rect position, SerializedProperty property)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(prop, true));
        EditorGUI.PropertyField(rect, prop, new GUIContent(name), true);
        position.y += EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float propertyHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty UseProgressSwitchProp = property.FindPropertyRelative(UseProgressSwitch);

        if (UseProgressSwitchProp.boolValue)
        {
            propertyHeight += HeightPlus(property, startSwitch);
            propertyHeight += HeightPlus(property, endSwitch);
        }

        return propertyHeight;
    }

    private float HeightPlus(SerializedProperty property, string name)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        return EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
    }
}