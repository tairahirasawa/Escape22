using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OpenCloseObject))] // この行を変更して、カスタマイズするクラス名に合わせます
public class OpenCloseObjectEditor : Editor
{
    SerializedProperty isLockProperty;
    //SerializedProperty gimmickProperty;

    private void OnEnable()
    {
        // SerializedPropertyオブジェクトへの参照を取得
        isLockProperty = serializedObject.FindProperty("IsLock");
        //gimmickProperty = serializedObject.FindProperty("gimmick");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 全ての公開プロパティを表示
        DrawPropertiesExcluding(serializedObject, "gimmick"); // "gimmick"を除外して他のプロパティを表示

        // IsLockがtrueの場合にのみgimmickを表示
        if (isLockProperty.boolValue)
        {
            //EditorGUILayout.PropertyField(gimmickProperty, new GUIContent("Gimmick"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
