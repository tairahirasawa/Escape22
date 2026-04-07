using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Gimmick))]
public class GimmickEditor : Editor
{
    #region SerializedProperites

    SerializedProperty gimmickType;
    SerializedProperty progressKey;
    SerializedProperty lockProgressKey;

    SerializedProperty needZoom;
    SerializedProperty always;
    SerializedProperty hideBG;
    SerializedProperty DisableClick;

    SerializedProperty NeedItems;

    SerializedProperty gimmickButtonType;
    SerializedProperty gimmickButtonNum;

    SerializedProperty answer;
    SerializedProperty gimmickButtonText;
    SerializedProperty gimmickButtonSprite;
    
    SerializedProperty eventActions;
    SerializedProperty scrCloseButtonActions;

    #endregion

    private void OnEnable()
    {
        gimmickType = serializedObject.FindProperty("gimmickType");
        progressKey = serializedObject.FindProperty("progressKey");
        lockProgressKey = serializedObject.FindProperty("lockProgressKey");
        needZoom = serializedObject.FindProperty("needZoom");
        always = serializedObject.FindProperty("always");
        hideBG = serializedObject.FindProperty("hideBG");
        DisableClick = serializedObject.FindProperty("DisableClick");
        NeedItems = serializedObject.FindProperty("NeedItems");
        gimmickButtonType = serializedObject.FindProperty("gimmickButtonType");
        gimmickButtonNum = serializedObject.FindProperty("gimmickButtonlayout");
        answer = serializedObject.FindProperty("answer");
        gimmickButtonText = serializedObject.FindProperty("gimmickButtonText");
        gimmickButtonSprite = serializedObject.FindProperty("gimmickButtonSprite");
        eventActions = serializedObject.FindProperty("eventActions");
        scrCloseButtonActions = serializedObject.FindProperty("scrCloseButtonActions");
    }

    public override void OnInspectorGUI()
    {

        Gimmick gimmick = (Gimmick)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(gimmickType);
        EditorGUILayout.PropertyField(progressKey);
        EditorGUILayout.PropertyField(lockProgressKey);
        EditorGUILayout.PropertyField(needZoom);
        EditorGUILayout.PropertyField(always);
        EditorGUILayout.PropertyField(hideBG);
        EditorGUILayout.PropertyField(DisableClick);


        if (gimmick.gimmickType == GimmickType.itemUse)
        {
            EditorGUILayout.PropertyField(NeedItems);
        }

        if(gimmick.gimmickType == GimmickType.sort || gimmick.gimmickType == GimmickType.order)
        {
            EditorGUILayout.PropertyField(gimmickButtonType);
            EditorGUILayout.PropertyField(gimmickButtonNum);
            EditorGUILayout.PropertyField(answer);

            if(gimmick.gimmickButtonType == GimickButtonType.textMesh)
            {
                EditorGUILayout.PropertyField(gimmickButtonText);
            }
            else if (gimmick.gimmickButtonType == GimickButtonType.sprite)
            {
                EditorGUILayout.PropertyField(gimmickButtonSprite);
            }
        }

        EditorGUILayout.PropertyField(eventActions);
        EditorGUILayout.PropertyField(scrCloseButtonActions);

        serializedObject.ApplyModifiedProperties();
        
    }
}
