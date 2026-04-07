using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;
using Cinemachine;
using Cinemachine.Utility;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using System.Linq;

public partial class GameData
{
    public string S_CurrentMapName;
    public List<string> S_MapHistory;
}


public class MapManager : MonoBehaviour
{
    public SerializableDictionaryBase<string, Map> maps;

    public string DefaultMapName;

    public Map currentMap;
    public Map visibleMap;
    public string currentMapName { get => ProgressSaveManger.GetProgressDatas(StageContext.currentStageType.ToString()).currentMapName; set => ProgressSaveManger.GetProgressDatas(StageContext.currentStageType.ToString()).currentMapName = value; }
    //public List<string> mapHistory { get => DataPersistenceManager.I.gameData.S_MapHistory; set => DataPersistenceManager.I.gameData.S_MapHistory = value; }

    public bool CanBackPreviousMap;

    public Camera mainCamera;
    public GameObject MainVcam;

    //Zoom関係
    public bool IsZoom;
    public List<Vector3> beforeZoomPos = new List<Vector3>();
    public List<float> beforeOrthoSize = new List<float>();

    public float orthoSize;
    private float ZoomTime = 0;// 0.3f;

    //Action
    public Func<UniTask> OnZoomIn;
    public Func<UniTask> OnZoomOut;
    public Func<UniTask> OnMapChange;

    public bool IsAllMapPresent;

    public async UniTask AllMapPresent()
    {
        foreach (var map in maps)
        {
            map.Value.gameObject.SetActive(true);
        }

        await UniTask.Yield();
    }

    public async Task SetUpMap()
    {
        orthoSize = MainVcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;

        //currentMapName = ProgressSaveManger.GetProgressDatas(StageContext.currentStageType.ToString()).currentMapName;

        if (currentMapName == null || currentMapName == "" || !IsExistMapName(currentMapName))
        {
            await ChangeMap(DefaultMapName);
        }
        else
        {
            if (maps[currentMapName].DontLoadThisMap)
            {
                currentMapName = maps[currentMapName].previousMapInfo.previousMap.myMapInfo.mapName;
            }


            await ChangeMap(currentMapName);
            LoadMap();
        }
    }


    public bool IsExistMapName(string name)
    {
        if(name == null || name =="")
        {
            return false;
        }
        else
        {
            foreach (var mapname in maps)
            {
                if(mapname.Key == name)
                {
                    return true;
                }
            }
        }

        return false;
    }


    public float MoveAmountX()
    {
        /*
        var vcam = MainVcam.GetComponent<CinemachineVirtualCamera>();
        var cam = mainCamera; 

        float halfHeight = vcam.m_Lens.OrthographicSize;
        float halfWidth = halfHeight * cam.aspect; 

        return halfWidth * 2; 
        */

        return 10.8f;
    }

    public float MoveAmountY()
    {
        /*
        var vcam = MainVcam.GetComponent<CinemachineVirtualCamera>();
        var cam = mainCamera;

        float halfWidth = vcam.m_Lens.OrthographicSize;

        float aspectRatioInverse = 1.0f / cam.aspect;

        float halfHeight = halfWidth * aspectRatioInverse;

        return halfHeight;
        */

        return 19.47f; // 全高を返す
    }

    public void StageChangeMainVcam(string mapName, bool dontSave = false)
    {
        if (dontSave == false)
        {
            currentMap = maps[mapName];
            OnMapChange?.Invoke();

        }
        else
        {
            SaveCurrentMap(mapName);
        }

        var camPos = MainVcam.transform.localPosition;

        var moveX = maps[mapName].currentIndexX * MoveAmountX();
        var moveY = maps[mapName].currentIndexY * MoveAmountY();

        MainVcam.transform.localPosition = new Vector3(moveX, moveY, camPos.z);

        Debug.Log(MainVcam.transform.localPosition);

        maps[mapName].gameObject.transform.position = new Vector3(0, 0, 0); //SetActive(true);
        visibleMap = maps[mapName];

        foreach (var map in maps)
        {
            if (map.Key != mapName)
            {
                map.Value.gameObject.transform.position = new Vector3(0, 100, 0); //SetActive(false);
            }
        }

        // EnvironmentalManager.I.UpdateAllContents();


    }

    /// <summary>
    /// イベント中はマップを保存しない為のメソッド
    /// </summary>
    /// <param name="mapName"></param>
    public async void SaveCurrentMap(string mapName)
    {
        await UniTask.WaitUntil(() => EventActionCreator.NowEventList.Count == 0);

        //Debug.Log("カウント" + EventActionCreator.NowEventList.Count);

        currentMap = maps[mapName];
        OnMapChange?.Invoke();
    }

    public async Task ChangeMap(string mapName,bool dontSave = false)
    {
        /*
        if(mapHistory == null || mapHistory.Count == 0)
        {
            mapHistory = new List<string>()
            {
                DefaultMapName
            };
        }

        if(mapHistory.Count >1)
        {
            if (mapHistory[1] == mapName)
            {
                mapHistory.RemoveAt(0);
            }
        }

        if (mapHistory[0] != mapName)
        {
            mapHistory.Insert(0, mapName);
        }
        */

        await UniTask.Yield();

        StageChangeMainVcam(mapName,dontSave);

    }


    public void ChangePreviousMap(PreviousMapInfo previousMapInfo)
    {
        //var mapName = mapHistory[1];
        if(previousMapInfo.previousMapIndex == 999)
        {
            StageChangeMainVcam(previousMapInfo.previousMap.myMapInfo.mapName);
        }
        else
        {

            maps[previousMapInfo.previousMap.myMapInfo.mapName].currentIndexX = previousMapInfo.previousMapIndex;
            StageChangeMainVcam(previousMapInfo.previousMap.myMapInfo.mapName);

        }


        //mapHistory.RemoveAt(0);
    }


    public void LoadMap()
    {
        var camPos = MainVcam.transform.localPosition;
        currentMap = maps[currentMapName];
        MainVcam.transform.localPosition = new Vector3(MoveAmountX() * currentMap.currentIndexX, MoveAmountY() * currentMap.currentIndexY, camPos.z);
    }

    public async UniTask Zoom(Vector3 target,float zoomValue, float zoomtime = 0,bool force = false)
    {
        if (IsZoom && !force) return;

        float positionTolerance = 0.001f; // 許容される位置の誤差
        if (Mathf.Abs(MainVcam.transform.position.x - target.x) <= positionTolerance &&
            Mathf.Abs(MainVcam.transform.position.y - target.y) <= positionTolerance &&
            Mathf.Abs(MainVcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize - (orthoSize * zoomValue)) <= positionTolerance
            )
        {
            return;
        }

        Debug.Log(MainVcam.transform.position + " " + target);
        


        MoveBtn.DisableMove = true;

 

        beforeZoomPos.Add(MainVcam.transform.position);
        beforeOrthoSize.Add(MainVcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize);


        MainVcam.transform.DOLocalMove(new Vector3(target.x, target.y, MainVcam.transform.position.z), zoomtime).SetEase(Ease.InOutSine);

        var virtualCamera = MainVcam.GetComponent<CinemachineVirtualCamera>();
        float startValue = virtualCamera.m_Lens.OrthographicSize; // 開始値
        float endValue = orthoSize * zoomValue; // 終了値

        if(zoomtime>0)
        {

            // DOTween の DOFloat メソッドを使って値を変化させる
            var tween = DOTween.To(() => startValue, x => startValue = x, endValue, zoomtime)
                   .SetEase(Ease.InOutSine)
                   .SetUpdate(true); // TweenのUpdateを有効にする

            tween.OnUpdate(() => {
                virtualCamera.m_Lens.OrthographicSize = startValue;
            });
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = endValue;
        }

        if(zoomtime > 0) await UniTask.Delay(Mathf.FloorToInt(zoomtime * 1000));

        IsZoom = true;
        MoveBtn.DisableMove = false;

        if (OnZoomIn != null)
        {
            await OnZoomIn();
        }


    }

    public async UniTask BackZoom()
    {
        if (IsZoom == false) return;

        MoveBtn.DisableMove = true;

        if (OnZoomOut != null)
        {
            await OnZoomOut();
        }

        MainVcam.transform.DOLocalMove(beforeZoomPos[beforeZoomPos.Count - 1], ZoomTime).SetEase(Ease.InOutSine);

        float startValue = MainVcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize; // 開始値
        float endValue = beforeOrthoSize[beforeOrthoSize.Count - 1]; // 終了値
        float duration = ZoomTime; // 変化にかかる時間（秒）

        // DOTween の DOFloat メソッドを使って値を変化させる
        DOTween.To(() => startValue, x => startValue = x, endValue, duration)
               .SetEase(Ease.InOutSine)
               .OnUpdate(() =>
               {
                   MainVcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = startValue;
                   beforeZoomPos.RemoveAt(beforeZoomPos.Count - 1);
                   beforeOrthoSize.RemoveAt(beforeOrthoSize.Count - 1);
               })
               .OnComplete(() =>
               {

                   if (beforeZoomPos.Count == 0)
                   {
                       IsZoom = false;

                   }

                   MoveBtn.DisableMove = false;
               });

        await UniTask.Yield();


        /*
        if(ZoomTime > 0 ) await UniTask.Delay(Mathf.FloorToInt(ZoomTime * 1000));

        if (beforeZoomPos.Count == 0)
        {
            IsZoom = false;

        }

        MoveBtn.DisableMove = false;
        */
   
    }


    public void CameraMoveX(float MoveValue)
    {

        MoveBtn.DisableMove = true;

        var camPos = MainVcam.transform.localPosition;
        MainVcam.transform.DOLocalMove(new Vector3(camPos.x + MoveValue, camPos.y, camPos.z), 0.3f).SetEase(Ease.InOutSine).OnComplete(()=>
        {
            OnMapChange?.Invoke();
            MoveBtn.DisableMove = false;

        });
    }

    public void CameraMoveY(float MoveValue)
    {
        MoveBtn.DisableMove = true;
        var camPos = MainVcam.transform.localPosition;
        MainVcam.transform.DOLocalMove(new Vector3(camPos.x, camPos.y + MoveValue, camPos.z), 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            OnMapChange?.Invoke();
            MoveBtn.DisableMove = false;

        });


    }


    public bool IsCurrentMap(Map[] maps)
    {
        for (int i = 0; i < maps.Length; i++) 
        {
            if (maps[i] == currentMap)
            {
                return true;
            }
        }

        return false;
    }

    public bool EnableRightMove()
    {
        if (currentMap == null) return false;
        if (IsZoom == true) return false;
        return currentMap.EnableRightMove();
    }

    public bool EnableLeftMove()
    {

        if (currentMap == null) return false;
        if (IsZoom == true) return false;
        return currentMap.EnableLeftMove();
    }

    public bool EnableDownMove()
    {

        if (currentMap == null) return false;
        if (IsZoom == true) return false;
        return currentMap.EnableDownMove();
    }

    public bool EnableUpMove()
    {

        if (currentMap == null) return false;
        if (IsZoom == true) return false;
        return currentMap.EnableUpMove();
    }

    private void Update()
    {
        if(currentMap != null)
        {
            //CanBackPreviousMap = currentMap.currentIndex == 0 && mapHistory.Count > 1;
            CanBackPreviousMap = currentMap.currentIndexX == 0 && !currentMap.DiasbleBackButton;
            currentMapName = currentMap.name;
        }
       
    }
}
