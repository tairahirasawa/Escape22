using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class VCamManager : SingletonMonoBehaviour<VCamManager>
{
    public Camera cam;
    public CinemachineVirtualCamera mainVcam;
    public List<CinemachineVirtualCamera> vcams;

    void Start()
    {
        // 現在のアクティブなカメラを取得
        try
        {
            CinemachineBrain cinemachineBrain = cam.GetComponent<CinemachineBrain>();
            
            if (cinemachineBrain != null)
            {
                mainVcam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
            }
        }
        catch
        {

        }

    }

    void Update()
    {

        try
        {
            // 現在のアクティブなカメラを更新
            CinemachineBrain cinemachineBrain = cam.GetComponent<CinemachineBrain>();
            if (cinemachineBrain != null)
            {
                mainVcam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
            }
        }
        catch
        {

        }

    }

    public async UniTask MoveMainVcam(Vector2 targetPos,float time)
    {
        mainVcam.transform.DOMove(new Vector3(targetPos.x,targetPos.y,mainVcam.transform.position.z),time);
        if(time>0) await UniTask.Delay(Mathf.FloorToInt(1000 * time));
    }

    public async UniTask ShakeCamera(float duration,float strength,int vibrato,bool fadeOut,float wait,bool infinity)
    {
        mainVcam.transform.DOKill();
        Vector3 originalPosition = mainVcam.transform.localPosition;
        int loopnum ;

        if (infinity == false)
        {
            loopnum = 1;
        }
        else
        {
            loopnum = -1;
        }

        // DoTweenを使ってカメラを揺らす
        mainVcam.transform.DOShakePosition(duration, strength, vibrato,90,false,fadeOut).SetLoops(loopnum).OnComplete(() =>
        {
            // アニメーションが完了したら、カメラを元の位置に戻す
            mainVcam.transform.localPosition = originalPosition;
        });

        await UniTask.Delay(Mathf.FloorToInt(wait * 1000));
    }

}
