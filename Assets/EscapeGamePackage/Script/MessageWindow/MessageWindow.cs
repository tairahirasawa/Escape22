using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using DG.Tweening;

public class MessageWindow : SingletonMonoBehaviour<MessageWindow>
{
    public CharactorData charactorData;
    public MessageWindowUI messageWindowUI;
    public ButtonContainerUI buttonContainerUI;
    public Message testMessage;

    public int TermIndex;

    public int waitDelay;

    protected override void OnAwake()
    {
        messageWindowUI.gameObject.SetActive(false);
        buttonContainerUI.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(LocalizationManager.CurrentLanguageCode == "en")
        {
            waitDelay = 5;
        }
        else
        {
            waitDelay = 20;
        }
    }

    public async UniTask PresentMessageWindowAsync(string CategoryName, params Message[] messages)
    {

        foreach (var message in messages)
        {
            messageWindowUI.charactorImage.sprite = charactorData.GetCharactorSprite(message.charactorType);
            messageWindowUI.charactorImage.gameObject.SetActive(message.showCharactorImage);
            messageWindowUI.gameObject.SetActive(true);

            string charaName = null;
            if (message.charactorName)
            {
                charaName = LocalizationManager.GetTranslation("Charactor/" + charactorData.GetCharactorTerm(message.charactorType));
            }

            if(message.messageAnimation.animationObject != null)
            {
                message.messageAnimation.DoAnimaton();
            }


            for (int i = 0; i < message.messageTerms.Length; i++)
            {
                var termName = "";

                if(CategoryName == null)
                {
                    termName = message.messageTerms[i].term;
                }
                else
                {
                    termName = CategoryName + "/" + message.messageTerms[i].term;
                }

                Debug.Log(termName);

                string mainMessage = LocalizationManager.GetTranslation(termName);

 
                await ShowTextGradually(charaName,mainMessage);

                //messageWindowUI.MessageText.text = charaName + mainMessage;

                if (message.presentChoose && i == message.messageTerms.Length-1)
                {
                    buttonContainerUI.PresentChooseButton(message.yesAction,message.noAction,message.dontShowNoButton);
                    await UniTask.WaitUntil(() => buttonContainerUI.IsPresentButtonContainer == false);
                    break;
                }

                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));

                AudioDirector.I.PlaySE(AudioDirector.SeType.button, null, 0.4f);

                await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0));
            }

            if (message.messageAnimation.animationObject != null)
            {
                message.messageAnimation.EndAnimation();
            }
        }

        messageWindowUI.gameObject.SetActive(false);
    }
    /*
    public async UniTask PresentMessageStageAdvanceAd(EventActionCreator eventActionCreator,string CategoryName, params Message[] messages)
    {

        foreach (var message in messages)
        {
            messageWindowUI.charactorImage.sprite = charactorData.GetCharactorSprite(message.charactorType);
            messageWindowUI.charactorImage.gameObject.SetActive(message.showCharactorImage);
            messageWindowUI.gameObject.SetActive(true);

            string charaName = null;
            if (message.charactorName)
            {
                charaName = LocalizationManager.GetTranslation("Charactor/" + charactorData.GetCharactorTerm(message.charactorType));
            }

            for (int i = 0; i < message.messageTerms.Length; i++)
            {
                var termName = "";

                if (CategoryName == null)
                {
                    termName = message.messageTerms[i].term;
                }
                else
                {
                    termName = CategoryName + "/" + message.messageTerms[i].term;

                }

                string mainMessage = LocalizationManager.GetTranslation(termName);


                await ShowTextGradually(charaName, mainMessage);

                //messageWindowUI.MessageText.text = charaName + mainMessage;

                if (message.presentChoose && i == message.messageTerms.Length - 1)
                {

                    buttonContainerUI.PresentStageAdvanceAdbuttonSet(eventActionCreator);
                    await UniTask.WaitUntil(() => buttonContainerUI.IsPresentButtonContainer == false);
                    break;
                }

                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));

                AudioDirector.I.PlaySE(AudioDirector.SeType.button, null, 0.4f);

                await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0));
            }
        }

        messageWindowUI.gameObject.SetActive(false);
    }
    */

    public async UniTask ShowTextGradually(string charaName, string tmpText)
    {

        if (tmpText != null)
        {
            // charaNameをtmpTextの前に付けて一気に表示
            string charaNameText;
            if(charaName == null)
            {
                charaNameText = "";
            }
            else
            {
                charaNameText = charaName+"\n";
            }

            string fullMessage = charaNameText + tmpText;
            int totalCharacters = fullMessage.Length;
            int startIndexOfTmpText = charaNameText.Length + 1; // charaNameと改行文字の長さを足した値

            messageWindowUI.MessageText.text = fullMessage;
            messageWindowUI.MessageText.maxVisibleCharacters = startIndexOfTmpText; // charaNameをすぐに表示

            for (int i = startIndexOfTmpText; i <= totalCharacters; i++)
            {
                messageWindowUI.MessageText.maxVisibleCharacters = i; // 表示する文字数を増やす

                if (Input.GetMouseButtonDown(0)) // マウスの左クリックを検知
                {
                    messageWindowUI.MessageText.maxVisibleCharacters = totalCharacters; // 全文字を表示
                    break; // ループを終了
                }

                if(i% 2 == 0)
                {
                    AudioDirector.I.PlaySE(AudioDirector.SeType.button, null, 0.4f);
                }

                await UniTask.Delay(waitDelay); // 0.02秒待つ

                // フレームの最後まで待機し、その間にマウスクリックがあったか確認
                await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
                if (Input.GetMouseButtonDown(0)) // マウスの左クリックを再度検知
                {
                    messageWindowUI.MessageText.maxVisibleCharacters = totalCharacters; // 全文字を表示
                    break; // ループを終了
                }
            }
        }
    }
}




[Serializable]
public class Message
{
    [SerializeField] public CharactorType charactorType;

    [SerializeField] public bool showCharactorImage;
    [SerializeField] public bool charactorName;
    [SerializeField] public MessageAnimation messageAnimation;
    [SerializeField] public bool presentChoose;
    [SerializeField] public bool dontShowNoButton;
    [SerializeField] public EventActionCreator yesAction;
    [SerializeField] public EventActionCreator noAction;

    [SerializeField] public messageTerm[] messageTerms;
}

[Serializable]
public class messageTerm
{
    public string term;
}

[Serializable]
public class MessageAnimation
{
    public GameObject animationObject;
    public AnimationType animationType;

    private Vector3 originPos;
    private Tween currentTween; // Tweenを保持する変数

    public enum AnimationType
    {
        none, jump
    }

    public void DoAnimaton()
    {
        originPos = animationObject.transform.position;

        switch (animationType)
        {
            case AnimationType.none:
                break;

            case AnimationType.jump:
                // Tweenを生成し、保持
                currentTween = animationObject.transform.DOMoveY(originPos.y + 1, 0.13f)
                    .SetLoops(-1, LoopType.Yoyo);
                break;
        }
    }

    public void EndAnimation()
    {
        switch (animationType)
        {
            case AnimationType.none:
                break;

            case AnimationType.jump:
                // ループを停止し、Tweenを終了
                if (currentTween != null && currentTween.IsActive())
                {
                    currentTween.Kill(); // Tweenを停止
                    currentTween = null;
                }
                // オブジェクトを元の位置に戻す
                animationObject.transform.DOMoveY(originPos.y, 0.13f);
                break;
        }
    }
}