using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Message))]
public class MessageDrawer : PropertyDrawer
{
    string charactorType = "charactorType";
    string showCharactorImage = "showCharactorImage";
    string charactorName = "charactorName";
    string messageAnimation = "messageAnimation";
    string presentChoose = "presentChoose";
    string dontShowNoButton = "dontShowNoButton";
    string yesAction = "yesAction";
    string noAction = "noAction"; // �������C�����܂���
    string messageTerms = "messageTerms";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Trigger Type����ɕ\��
        position.y += PresentProperty(charactorType, position, property);
        position.y += PresentProperty(showCharactorImage, position, property);
        position.y += PresentProperty(charactorName, position, property);
        position.y += PresentProperty(messageAnimation, position, property);

        // UseProgressSwitch�v���p�e�B�̕\��
        SerializedProperty presentChooseProp = property.FindPropertyRelative(presentChoose);
        position.y += PresentProperty(presentChoose, position, property);

        if (presentChooseProp.boolValue)
        {
            EditorGUI.indentLevel++;

            //position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            position.y += PresentProperty(dontShowNoButton, position, property);
            position.y += PresentProperty(yesAction, position, property);
            position.y += PresentProperty(noAction, position, property);

            EditorGUI.indentLevel--;
        }

        position.y += PresentProperty(messageTerms, position, property);

        EditorGUI.EndProperty();
    }

    public float PresentProperty(string name, Rect position, SerializedProperty property)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        float height = EditorGUI.GetPropertyHeight(prop, true);
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, height), prop, new GUIContent(ObjectNames.NicifyVariableName(name)), true);
        return height + EditorGUIUtility.standardVerticalSpacing;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float propertyHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty presentChooseProp = property.FindPropertyRelative(presentChoose);

        propertyHeight += HeightPlus(property, charactorType);
        propertyHeight += HeightPlus(property, showCharactorImage);
        propertyHeight += HeightPlus(property, charactorName);
        propertyHeight += HeightPlus(property, messageAnimation);
        propertyHeight += HeightPlus(property, presentChoose);

        if (presentChooseProp.boolValue)
        {
            propertyHeight += HeightPlus(property, dontShowNoButton);
            propertyHeight += HeightPlus(property, yesAction);
            propertyHeight += HeightPlus(property, noAction);
        }

        propertyHeight += HeightPlus(property, messageTerms);

        return propertyHeight;
    }

    private float HeightPlus(SerializedProperty property, string name)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        return EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
    }
}
