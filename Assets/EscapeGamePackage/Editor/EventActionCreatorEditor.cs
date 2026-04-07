using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventActionCreator))]
public class EventActionCreatorEditor : Editor
{
    /*
    #region SerializedProperites

    SerializedProperty eventActionTrigger;
    SerializedProperty 
    #endregion

    private void OnEnable()
    {
        eventActionTrigger = serializedObject.FindProperty("eventActionTrigger");
    }

    public override void OnInspectorGUI()
    {

        EventActionCreator gimmick = (EventActionCreator)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(eventActionTrigger);

        /*
        if (gimmick.gimmickType == GimmickType.itemUse)
        {
            EditorGUILayout.PropertyField(NeedItems);
        }

        if (gimmick.gimmickType == GimmickType.sort || gimmick.gimmickType == GimmickType.order)
        {
            EditorGUILayout.PropertyField(gimmickButtonType);
            EditorGUILayout.PropertyField(gimmickButtonNum);
            EditorGUILayout.PropertyField(answer);

            if (gimmick.gimmickButtonType == GimickButtonType.textMesh)
            {
                EditorGUILayout.PropertyField(gimmickButtonText);
            }

            if (gimmick.gimmickButtonType == GimickButtonType.sprite)
            {
                EditorGUILayout.PropertyField(gimmickButtoSprite);
            }
        }
     
        serializedObject.ApplyModifiedProperties();
           */
}

