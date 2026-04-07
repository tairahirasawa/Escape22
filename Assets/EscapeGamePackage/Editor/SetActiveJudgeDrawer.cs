using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SetActiveJudge))]
public class SetActiveJudgeDrawer : PropertyDrawer
{
    string UseSetActiveJudge = "UseSetActiveJudge";
    string setActiveSets = "setActiveSets";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // UseProgressSwitchプロパティの表示
        SerializedProperty UseProgressSwitchProp = property.FindPropertyRelative(UseSetActiveJudge);
        Rect useProgressSwitchRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(useProgressSwitchRect, UseProgressSwitchProp, new GUIContent("UseSetActiveJudge"));

        if (UseProgressSwitchProp.boolValue)
        {

            EditorGUI.indentLevel++;

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            PresentProperty(setActiveSets, ref position, property);

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
        SerializedProperty UseProgressSwitchProp = property.FindPropertyRelative(UseSetActiveJudge);

        
        if (UseProgressSwitchProp.boolValue)
        {
            propertyHeight += HeightPlus(property, setActiveSets);
        }
        
        return propertyHeight;
    }

    private float HeightPlus(SerializedProperty property, string name)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        return EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
    }
}
