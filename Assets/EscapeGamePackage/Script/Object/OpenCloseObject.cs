using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCloseObject : MonoBehaviour, IPointerClickHandler
{
    private Sprite closeSprite;
    public Sprite openSprite;
    public BoxStateType stateType;
    public AudioDirector.SeType seType;

    public GameObject itemContainer;
    public GameObject CloseSpriteAccessories;

    public bool needZoom;
    public bool IsLock;
    public bool KeepOpen;

    //ここからギミックの設定
    //[SerializeField] private Gimmick gimmick;

    private void Awake()
    {
        closeSprite = GetComponent<SpriteRenderer>().sprite;
        
        stateType = BoxStateType.Close;
        GetComponent<SpriteRenderer>().sprite = closeSprite;
        if(itemContainer != null) itemContainer.SetActive(false);

        //gimmick.ClearAction = UnLockAction;
    }

    private void Start()
    {
        //if (IsLock) IsLock = !gimmick.IsDoneProgress;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsLock) return;
        if (KeepOpen) return;
        if (needZoom && !StageSwitcher.I.mapManager.IsZoom) return;
        ChangeBoxState();
    }

    public async UniTask UnLockAction()
    {
        AudioDirector.I.PlaySE(AudioDirector.SeType.KeyUnlock);

        await UniTask.Delay(1000);

        IsLock = false;
        KeepOpen = true;
        ChangeBoxState();
    }

    public void ChangeBoxState()
    {
        switch(stateType)
        {
            case BoxStateType.Close:

                stateType = BoxStateType.Open;
                AudioDirector.I.PlaySE(seType);
                GetComponent<SpriteRenderer>().sprite = openSprite;
                if(CloseSpriteAccessories != null) CloseSpriteAccessories.SetActive(false);
                if (itemContainer != null) itemContainer.SetActive(true);
                break;

            case BoxStateType.Open:
                stateType = BoxStateType.Close;
                AudioDirector.I.PlaySE(seType);
                GetComponent<SpriteRenderer>().sprite = closeSprite;
                if (CloseSpriteAccessories != null) CloseSpriteAccessories.SetActive(true);
                if (itemContainer != null) itemContainer.SetActive(false);
                break;
        }
    }

    public void LoadOpen()
    {
        KeepOpen = true;
        stateType = BoxStateType.Open;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        if (CloseSpriteAccessories != null) CloseSpriteAccessories.SetActive(false);
        if (itemContainer != null) itemContainer.SetActive(true);
    }


    public enum BoxStateType
    { 
       Close,Open,
    }

}
