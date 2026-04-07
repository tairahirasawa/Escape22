using Cysharp.Threading.Tasks.Triggers;
using UnityEditor;
using UnityEngine;
using Cinemachine;

[InitializeOnLoad] // エディタがロードされた時にクラスを初期化
public class KeyInputEditor
{
    static KeyInputEditor()
    {
        SceneView.duringSceneGui += sceneView =>
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftArrow)
            {
                var mapmanager = GameObject.FindAnyObjectByType<MapManager>().GetComponent<MapManager>();
                mapmanager.MainVcam.transform.localPosition += new Vector3(mapmanager.MoveAmountX() * -1, 0, 0); 
                Event.current.Use(); // イベントを使用済みとする
            }

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.RightArrow)
            {
                var mapmanager = GameObject.FindAnyObjectByType<MapManager>().GetComponent<MapManager>();
                mapmanager.MainVcam.transform.localPosition += new Vector3(mapmanager.MoveAmountX(), 0, 0);
                Event.current.Use(); // イベントを使用済みとする
            }

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
            {
                var mapmanager = GameObject.FindAnyObjectByType<MapManager>().GetComponent<MapManager>();
                mapmanager.MainVcam.transform.localPosition += new Vector3(0, mapmanager.MoveAmountY()*-1, 0);
                Event.current.Use(); // イベントを使用済みとする
            }

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
            {
                var mapmanager = GameObject.FindAnyObjectByType<MapManager>().GetComponent<MapManager>();
                mapmanager.MainVcam.transform.localPosition += new Vector3(0, mapmanager.MoveAmountY(), 0);
                Event.current.Use(); // イベントを使用済みとする
            }
        };
    }

}