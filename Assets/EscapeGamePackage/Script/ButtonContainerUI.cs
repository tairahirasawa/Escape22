using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainerUI : MonoBehaviour
{
    public Button adButton;
    public Button yesButton;
    public Button noButton;

    public bool IsPresentButtonContainer;

    private void Awake()
    {
        adButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    public void PresentChooseButton(EventActionCreator yesAction, EventActionCreator noAction = null,bool donshowNoButton = false)
    {
        
        if (ProgressDirector.I.IsDoneProgress(yesAction.eventActionConfig.progressKey))
        {
            PresentNormalbuttonSet(yesAction, noAction, donshowNoButton);
        }
        else
        {
            PresentAdbuttonSet(yesAction, noAction, donshowNoButton);
        }
    }


    public void PresentAdbuttonSet(EventActionCreator yesAction, EventActionCreator noAction = null,bool donshowNoButton = false)
    {
        IsPresentButtonContainer = true;

        adButton.gameObject.SetActive(true);

        if (donshowNoButton == false)
        {
            noButton.gameObject.SetActive(true);
        }
        else
        {
            noButton.gameObject.SetActive(false);
        }  


        adButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        adButton.onClick.AddListener(() => GoogleAdManager.I.googleAD_RewardSubEvent.ShowRewardedAd(async ()=> await YesButtonAction(yesAction)));

        if (noAction == null)
        {
            noButton.onClick.AddListener(() => HideButtons());
        }
        else
        {
            noButton.onClick.AddListener(async () => await noAction.EventAction());
        }
    }

    /*
    public void PresentStageAdvanceAdbuttonSet(EventActionCreator eventActionCreator)
    {
        IsPresentButtonContainer = true;



        adButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

        adButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        adButton.onClick.AddListener(() => GoogleAdManager.I.googleAD_RewardStageAdvance.ShowRewardedAd(()=>HideButtons()));
        noButton.onClick.AddListener(() =>
        {
            eventActionCreator.CanselEvent();
            HideButtons();
        } );
    }
    */

    public void PresentNormalbuttonSet(EventActionCreator yesAction, EventActionCreator noAction = null,bool donshowNoButton = false)
    {
        IsPresentButtonContainer = true;

        yesButton.gameObject.SetActive(true);

        Debug.Log(donshowNoButton);

        if (donshowNoButton == false)
        {
            noButton.gameObject.SetActive(true);
        }
        else
        {
            noButton.gameObject.SetActive(false);
        }   

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(async()=> await YesButtonAction(yesAction));

        if(noAction == null)
        {
            noButton.onClick.AddListener(() => HideButtons());
        }
        else
        {
            noButton.onClick.AddListener(async () => await noAction.EventAction());
        }
    }

    public async UniTask YesButtonAction(EventActionCreator yesActoin)
    {
        if(!ProgressDirector.I.IsDoneProgress(yesActoin.eventActionConfig.progressKey))
        {
            ProgressDirector.I.DoneProgress(yesActoin.eventActionConfig.progressKey);
        }

        adButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        await yesActoin.EventAction();

        IsPresentButtonContainer = false;

    }

    public void HideButtons()
    {

        MessageWindow.I.messageWindowUI.gameObject.SetActive(false);

        adButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        IsPresentButtonContainer = false;
    }



}
