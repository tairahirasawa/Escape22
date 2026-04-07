using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GimmickButtonLayout))]
public class GimmickButtonLayoutDrawer : PropertyDrawer
{
    string IsUseCustomGimmickButton = "IsUseCustomGimmickButton";
    string titleImage = "titleImage";
    string titleText = "titleText";
    string titleTerm = "titleTerm";
    string titleTextColor = "titleTextColor";
    string NumX = "NumX";
    //string NumY = "NumY";
    string CustomPadding = "CustomPadding";
    string padding = "padding";
    string hideArrowButton = "hideArrowButton";
    string gimmickButtonStyles = "gimmickButtonStyles";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 各プロパティを表示し、positionを更新
        position = PresentProperty(titleImage, position, property);
        position = PresentProperty(titleText, position, property);
        position = PresentProperty(titleTerm, position, property);
        position = PresentProperty(titleTextColor, position, property);
        position = PresentProperty(NumX, position, property);
        //position = PresentProperty(NumY, position, property);
        position = PresentProperty(CustomPadding, position, property);

        SerializedProperty customPaddingProp = property.FindPropertyRelative(CustomPadding);
        if (customPaddingProp.boolValue)
        {
            position = PresentProperty(padding, position, property);
        }


        position = PresentProperty(hideArrowButton, position, property);

        SerializedProperty isUseCustomGimmickButtonProp = property.FindPropertyRelative(IsUseCustomGimmickButton);
        EditorGUI.PropertyField(position, isUseCustomGimmickButtonProp, new GUIContent("IsUseCustomGimmickButton"), true);
        position.y += EditorGUI.GetPropertyHeight(isUseCustomGimmickButtonProp, true) + EditorGUIUtility.standardVerticalSpacing;

        if (isUseCustomGimmickButtonProp.boolValue)
        {
            position = PresentProperty(gimmickButtonStyles, position, property);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float propertyHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        propertyHeight += HeightPlus(property, titleImage);
        propertyHeight += HeightPlus(property, titleText);
        propertyHeight += HeightPlus(property, titleTerm);
        propertyHeight += HeightPlus(property, titleTextColor);
        propertyHeight += HeightPlus(property, NumX);
        //propertyHeight += HeightPlus(property, NumY);
        propertyHeight += HeightPlus(property, CustomPadding);
        propertyHeight += HeightPlus(property, hideArrowButton);

        SerializedProperty isUseCustomGimmickButtonProp = property.FindPropertyRelative(IsUseCustomGimmickButton);
        if (isUseCustomGimmickButtonProp.boolValue) // IsUseCustomGimmickButtonがtrueの場合にのみ、追加の高さを加算
        {

            propertyHeight += HeightPlus(property, gimmickButtonStyles);
        }

        SerializedProperty customPaddingProp = property.FindPropertyRelative(CustomPadding);
        if (customPaddingProp.boolValue)
        {
            propertyHeight += HeightPlus(property, padding);
        }

        return propertyHeight;
    }

    private Rect PresentProperty(string name, Rect position, SerializedProperty property)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        Rect propertyPosition = new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(prop, true));
        EditorGUI.PropertyField(propertyPosition, prop, new GUIContent(name), true);
        position.y += EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
        return position; // 更新されたpositionを返す
    }
    private float HeightPlus(SerializedProperty property, string name)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        return EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
    }
}
 