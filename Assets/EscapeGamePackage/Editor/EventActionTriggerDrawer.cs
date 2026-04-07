using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[CustomPropertyDrawer(typeof(EventActionTrigger))]
public class EventActionTriggerDrawer : PropertyDrawer
{
    string triggerType = "triggerType";
    string needZoom = "needZoom";
    string always = "always";
    string dontStopIsDoneProgress = "dontStopIsDoneProgress";
    string needItem = "needItem";
    string DontDeleteUseItem = "DontDeleteUseItem";
    string presentUnDoneMessage = "presentUnDoneMessage";
    string needItemTypes = "needItemTypes";
    string UnDoneMessage = "UnDoneMessage";
    string targetSetting = "targetSetting";
    string DontStartNotShowStageScreen = "DontStartNotShowStageScreen";
    string gimmick = "gimmick";
    string targetMap = "targetMap";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Trigger Typeを常に表示
        SerializedProperty eventType = property.FindPropertyRelative(triggerType);
        position.y += PresentProperty(triggerType, position, property);

        // PointerClickの場合のロジック
        if (eventType.enumValueIndex == (int)EventActionTriggerType.PointerClick)
        {
            position.y += PresentProperty(needZoom, position, property);
            position.y += PresentProperty(always, position, property);
            position.y += PresentProperty(dontStopIsDoneProgress, position, property);
            position.y += PresentProperty(presentUnDoneMessage, position, property);

            SerializedProperty presentUnDoneMessageProp = property.FindPropertyRelative(presentUnDoneMessage);
            if (presentUnDoneMessageProp.boolValue)
            {
                position.y += PresentProperty(UnDoneMessage, position, property);
            }

            position.y += PresentProperty(needItem, position, property);

            SerializedProperty needItemProp = property.FindPropertyRelative(needItem);
            if (needItemProp.boolValue)
            {
                position.y += PresentProperty(DontDeleteUseItem, position, property);
                position.y += PresentProperty(needItemTypes, position, property);
            }
        }

        // ProgressStart と ProgressEnd の場合のロジック
        if (eventType.enumValueIndex == (int)EventActionTriggerType.ProgressStart || eventType.enumValueIndex == (int)EventActionTriggerType.ProgressEnd)
        {
            position.y += PresentProperty(targetSetting, position, property);
            position.y += PresentProperty(DontStartNotShowStageScreen, position, property);
        }

        // Gimmickの場合のロジック
        if (eventType.enumValueIndex == (int)EventActionTriggerType.Gimmick)
        {
            position.y += PresentProperty(gimmick, position, property);
        }

        // MapChangeの場合のロジック
        if(eventType.enumValueIndex == (int)EventActionTriggerType.MapChange)
        {
            position.y += PresentProperty(targetMap, position, property);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float propertyHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty eventType = property.FindPropertyRelative(triggerType);

        // 各eventTypeに応じたプロパティの高さを計算
        if (eventType.enumValueIndex == (int)EventActionTriggerType.PointerClick)
        {
            propertyHeight += HeightPlus(property, needItem);
            propertyHeight += HeightPlus(property, presentUnDoneMessage);

            SerializedProperty presentUnDoneMessageProp = property.FindPropertyRelative(presentUnDoneMessage);
            if (presentUnDoneMessageProp.boolValue) // needItemがtrueの場合にのみ、needItemTypesの高さを加算
            {
                propertyHeight += HeightPlus(property, UnDoneMessage);
            }


            propertyHeight += HeightPlus(property, needZoom);
            propertyHeight += HeightPlus(property, always);
            propertyHeight += HeightPlus(property, dontStopIsDoneProgress);
            SerializedProperty needItemProp = property.FindPropertyRelative(needItem);
            if (needItemProp.boolValue) // needItemがtrueの場合にのみ、needItemTypesの高さを加算
            {
                propertyHeight += HeightPlus(property, DontDeleteUseItem);
                propertyHeight += HeightPlus(property, needItemTypes);
            }
        }
        // 他のeventTypeに対する高さの計算
        if (eventType.enumValueIndex == (int)EventActionTriggerType.ProgressStart) propertyHeight += HeightPlus(property, targetSetting);
        if (eventType.enumValueIndex == (int)EventActionTriggerType.ProgressEnd) propertyHeight += HeightPlus(property, targetSetting);
        if (eventType.enumValueIndex == (int)EventActionTriggerType.ProgressStart) propertyHeight += HeightPlus(property, DontStartNotShowStageScreen);
        if (eventType.enumValueIndex == (int)EventActionTriggerType.ProgressEnd) propertyHeight += HeightPlus(property, DontStartNotShowStageScreen);
        if (eventType.enumValueIndex == (int)EventActionTriggerType.Gimmick) propertyHeight += HeightPlus(property, gimmick);
        if (eventType.enumValueIndex == (int)EventActionTriggerType.MapChange) propertyHeight += HeightPlus(property, targetMap);

        return propertyHeight;
    }

    public float PresentProperty(string name, Rect position, SerializedProperty property)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        float height = EditorGUI.GetPropertyHeight(prop, true);
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, height), prop, new GUIContent(name), true);
        return height + EditorGUIUtility.standardVerticalSpacing;
    }

    public float HeightPlus(SerializedProperty property, string name)
    {
        SerializedProperty prop = property.FindPropertyRelative(name);
        return EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
    }
}