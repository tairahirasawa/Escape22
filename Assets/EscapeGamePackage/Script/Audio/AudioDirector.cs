using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using Cysharp.Threading.Tasks;

public partial class GameData
{
    public bool S_Played;

    public bool S_BGM;
    public bool S_SE;
}

[Serializable]
public class AudioSettings
{
    public AudioSource audio;
    public float defaultValume;
} 


[Serializable]
public class BgmSettings
{
    public AudioSettings[] bgms;
    public bool NowPlay;

    public void playAllBGM()
    {
        NowPlay = true;

        for (int i = 0; i < bgms.Length; i++)
        {
            bgms[i].audio.volume = bgms[i].defaultValume;
            bgms[i].audio.Play();
        }
    }
    
    public void stopAllBGM()
    {
        NowPlay = true;

        for (int i = 0; i < bgms.Length; i++)
        {
            bgms[i].audio.volume = bgms[i].defaultValume;
            bgms[i].audio.Stop();
        }
    }

    public void ResumeAllBGM()
    {
        if (NowPlay)
        {
            for (int i = 0; i < bgms.Length; i++)
            {
                bgms[i].audio.volume = bgms[i].defaultValume;
                bgms[i].audio.Play();
            }
        }
    }

}

public class AudioDirector : SingletonMonoBehaviour<AudioDirector>
{
    public SerializableDictionaryBase<BGMType, BgmSettings> bgmsettings;
    public AudioSource seAudioSource;

    public AudioClip buttonSE, itemGetSE, gimmickSE, KeyUnlockSE,OpenSE,PopSE,WindowSE,DigSE,SetFireSE,SeikaiSE,FailerSE,FanfareSE,HartSE,PaperSE,PutOnSE,InjectionSE,PopKirakiraSE,OpenSE2,EnergySE,hungrySE,MessageSE;

    public Toggle bgmToggle, seToggel;

    public bool Admute;

    public enum BGMType
    {
        Main,Ending,Rain,OnlyWave
    }

    public enum SeType
    {
        none,button,itemGet,gimmick,KeyUnlock,Open,Pop,Window,Dig,SetFire,Seikai,Failer,Fanfare,Hart,Paper,PutOn,Injection,PopKirakira,Open2,Energy,Hungry,Message
    }

    protected override void OnAwake()
    {
        foreach (var setting in bgmsettings)
        {
            foreach (var bgmsetting in setting.Value.bgms)
            {
                bgmsetting.defaultValume = bgmsetting.audio.volume;
            }
        }
    }

    protected override void OnStart()
    {
        base.OnStart();

        if(DataPersistenceManager.I.gameData.S_Played == true)
        {
            bgmToggle.isOn = DataPersistenceManager.I.gameData.S_BGM;
            seToggel.isOn = DataPersistenceManager.I.gameData.S_SE;
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            PlayBGM(BGMType.Main);
        }


        if (bgmsettings == null) return;
        if(Admute == false)
        {
            
            foreach (var setting in bgmsettings)
            {
                foreach (var bgm in setting.Value.bgms)
                {
                    bgm.audio.mute = !bgmToggle.isOn;
                }
            }
            seAudioSource.mute = !seToggel.isOn;
            
        }


        DataPersistenceManager.I.gameData.S_BGM = bgmToggle.isOn;
        DataPersistenceManager.I.gameData.S_SE = seToggel.isOn;

    }

    public void forceMute()
    {
        Admute = true;

        foreach (var setting in bgmsettings)
        {  
            foreach (var bgm in setting.Value.bgms)
            {
                bgm.audio.mute = true;
            }
            
        }
        seAudioSource.mute = true;
    }

    public void forceMuteOff()
    {
        Admute = false;
    }

    public void PlaySE(SeType seType,AudioClip custom = null,float volume = 1,float startTime = 0)
    {
        if (seType == SeType.none && custom == null) return;

        if (seAudioSource.clip != null)
        {
            seAudioSource.clip = custom;
            seAudioSource.time = startTime;
            seAudioSource.volume = volume;
        }

        if (custom != null)
        {
            seAudioSource.PlayOneShot(custom, volume);
        }

        switch (seType)
        {
            case SeType.none:
                break;

            case SeType.button:
                seAudioSource.PlayOneShot(buttonSE, volume);
                break;

            case SeType.itemGet:
                seAudioSource.PlayOneShot(itemGetSE, volume);
                break;

            case SeType.gimmick:
                seAudioSource.PlayOneShot(gimmickSE, volume);
                break;

            case SeType.KeyUnlock:
                seAudioSource.PlayOneShot(KeyUnlockSE, volume);
                break;

            case SeType.Open:
                seAudioSource.PlayOneShot(OpenSE, volume);
                break;

            case SeType.Pop:
                seAudioSource.PlayOneShot(PopSE, volume);
                break;

            case SeType.Window:
                seAudioSource.PlayOneShot(WindowSE, volume);
                break;

            case SeType.Dig:
                seAudioSource.PlayOneShot(DigSE, volume);
                break;

            case SeType.SetFire:
                seAudioSource.PlayOneShot(SetFireSE, volume);
                break;

            case SeType.Seikai:
                seAudioSource.PlayOneShot(SeikaiSE, volume);
                break;

            case SeType.Failer:
                seAudioSource.PlayOneShot(FailerSE, volume);
                break;

            case SeType.Fanfare:
                seAudioSource.PlayOneShot(FanfareSE, volume);
                break;

            case SeType.Hart:
                seAudioSource.PlayOneShot(HartSE, volume);
                break;

            case SeType.Paper:
                seAudioSource.PlayOneShot(PaperSE, volume);
                break;

            case SeType.PutOn:
                seAudioSource.PlayOneShot(PutOnSE, volume);
                break;

            case SeType.Injection:
                seAudioSource.PlayOneShot(InjectionSE, volume);
                break;

            case SeType.PopKirakira:
                seAudioSource.PlayOneShot(PopKirakiraSE, volume);
                break;


            case SeType.Open2:
                seAudioSource.PlayOneShot(OpenSE2, volume);
                break;

            case SeType.Energy:
                seAudioSource.PlayOneShot(EnergySE, volume);
                break;

            case SeType.Hungry:
                seAudioSource.PlayOneShot(hungrySE, volume);
                break;


            case SeType.Message:
                seAudioSource.PlayOneShot(MessageSE, volume);
                break;
        }
        

    }

    public void RePlayFromStartAllBGM()
    {
        foreach (var setting in bgmsettings)
        {
            setting.Value.ResumeAllBGM();
        }
    }

    public async void PlayBGM(BGMType type,float changeduration = 0,bool dontStopOtherBGM = false)
    {

        if (bgmsettings == null) return;

        if (!dontStopOtherBGM)
        {
            foreach (var setting in bgmsettings)
            {
                foreach (var bgm in setting.Value.bgms)
                {

                    if (setting.Value != bgmsettings[type])
                    {
                        bgm.audio.DOFade(0, changeduration);
                    }

                }
            }
        }
 
        await UniTask.Delay(Mathf.FloorToInt(changeduration * 1000));


        bgmsettings[type].playAllBGM();

    }

    public void FadeoutBGM(float duration)
    {
        foreach (var setting in bgmsettings)
        {
            foreach (var bgm in setting.Value.bgms)
            {
                bgm.audio.DOFade(0, duration);
            }
        }
        
    }

}
