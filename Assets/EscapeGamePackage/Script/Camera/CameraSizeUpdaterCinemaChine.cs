//  CameraSizeUpdater.cs
//  http://kan-kikuchi.hatenablog.com/entry/CameraSizeUpdater
//
//  Created by kan.kikuchi on 2019.07.02.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// カメラのOrthographicSizeをアス比に応じて更新するクラス
/// </summary>
/// 

[ExecuteInEditMode]
public class CameraSizeUpdaterCinemaChine : MonoBehaviour
{

    public VCamManager vCam;
    //public Master master;
    //private Camera _camera;

    //縦、横、もしくは両方のどれを基準にするか
    private enum BaseType
    {
        Both, Width, Height
    }

    [SerializeField]
    private BaseType _baseType = BaseType.Both;

    //基準の画面サイズ
    [SerializeField]
    private float _baseWidth = 1080, _baseHeight = 1920;

    //画像のPixel Per Unit
    [SerializeField]
    private float _pixelPerUnit = 100f;

    //常に(Update中も)更新するか
    [SerializeField]
    private bool _isAlwaysUpdate = false;

    //現在のアス比
    private float _currentAspect;

    //=================================================================================
    //初期化
    //=================================================================================

    private void Awake()
    {
        //GetCurrentVirtualCamera();
        UpdateOrthographicSize();
    }

    //インスペクターの値が変更された時実行、OrthographicSizeを強制的に更新する
    private void OnValidate()
    {
        _currentAspect = 100000;
        //GetCurrentVirtualCamera();
        UpdateOrthographicSize();
    }

    //=================================================================================
    //更新
    //=================================================================================

    
    private void Update()
    {
        UpdateOrthographicSize();

        if (!_isAlwaysUpdate && Application.isPlaying)
        {
            return;
        }

        //GetCurrentVirtualCamera();


        /*
        switch(master.platForm)
        {
            case Master.PlatForm.SmartPhone:

                _baseType = BaseType.Width;

                _baseHeight = 1920;
                _baseWidth = 1080;
                
                break;

            case Master.PlatForm.WebGL:

                _baseType = BaseType.Height;

                _baseHeight = 1080;
                _baseWidth = 1080;

                break;

        }
        */

    }

    //カメラのOrthographicSizeをアス比に応じて更新
    private void UpdateOrthographicSize()
    {
        //現在のアスペクト比を取得し、変化がなければ更新しない

        if (vCam == null) return;

        
        float currentAspect = (float)Screen.height / (float)Screen.width;

        
        if (Mathf.Approximately(_currentAspect, currentAspect))
        {
            return;
        }
        
        _currentAspect = currentAspect;


        //カメラを取得していなければ取得
        /*
        if (_camera == null)
        {
            _camera = gameObject.GetComponent<Camera>();
        }
        */

        //基準のアスペクト比と、基準のアスペクト比の時のSize
        float baseAspect = _baseHeight / _baseWidth;
        float baseOrthographicSize = _baseHeight / _pixelPerUnit / 2f;

        //カメラのorthographicSizeを設定しなおす
 
        if (_baseType == BaseType.Height || (baseAspect > _currentAspect && _baseType != BaseType.Width))
        {
            //_camera.orthographicSize = baseOrthographicSize;

            vCam.mainVcam.m_Lens.OrthographicSize = baseOrthographicSize;
        }
        else
        {
            //_camera.orthographicSize = baseOrthographicSize * (_currentAspect / baseAspect);

            vCam.mainVcam.m_Lens.OrthographicSize = baseOrthographicSize * (_currentAspect / baseAspect);
        }

    }
    
}