using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugEditMove))]//拡張するクラスを指定
public class DebugEditModeLayout : Editor
{
    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        DebugEditMove script = target as DebugEditMove;

        //PublicMethodを実行する用のボタン
        if (GUILayout.Button("←"))
        {
            var camPos = GameObject.Find("MainVcam").transform.localPosition;
            GameObject.Find("MainVcam").transform.localPosition = new Vector3(camPos.x + 1080 * -1, camPos.y, camPos.z);
            //script.moveBtn.CameraMove(Screen.width * 1);
            //exampleScript.PublicMethod();
        }

        if (GUILayout.Button("→"))
        {
            var camPos = GameObject.Find("MainVcam").transform.localPosition;
            GameObject.Find("MainVcam").transform.localPosition = new Vector3(camPos.x + 1080, camPos.y, camPos.z);
            //script.moveBtn.CameraMove(Screen.width * -1);
            //exampleScript.PublicMethod();
        }

    }

}