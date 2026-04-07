using UnityEngine;
using UnityEngine.EventSystems;

public class TapSetActiveFalseMySelf : MonoBehaviour,IPointerClickHandler
{
    public AudioDirector.SeType seType;

    private void Start()
    {
        if(!GetComponent<BoxCollider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioDirector.I.PlaySE(seType);    
        this.gameObject.SetActive(false);
    }
}
