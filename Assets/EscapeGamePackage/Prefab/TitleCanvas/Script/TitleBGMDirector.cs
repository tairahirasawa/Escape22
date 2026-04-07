using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBGMDirector : SingletonMonoBehaviour<TitleBGMDirector>
{
    public List<AudioSource> audioSource;
    public Toggle bgmToggle;
    

    protected override void OnStart()
    {
        base.OnStart();

        if(DataPersistenceManager.I.gameData.S_Played == true)
        {
            bgmToggle.isOn = DataPersistenceManager.I.gameData.S_BGM;
        }

    }


    public void PlayTilteBGM()
    {
        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].Play();
        }
    }

    public void forceMute()
    {
        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].volume = 0;
        }

    }

    public void DisableMute()
    {
        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].volume = 0.5f;
        }
    }

    private void Update()
    {
        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].mute = !DataPersistenceManager.I.gameData.S_BGM;
        }

        DataPersistenceManager.I.gameData.S_BGM = bgmToggle.isOn;
    }
}
