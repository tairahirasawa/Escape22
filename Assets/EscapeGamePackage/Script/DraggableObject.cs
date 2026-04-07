using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(Collider2D))] // 2D用。3Dなら Collider に変更
public class DraggableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 offset;
    public Camera mainCamera;
    public bool needZoom;
    public bool isDragging = false;


    public void OnPointerDown(PointerEventData eventData)
    {

        isDragging = true;

        Vector3 mouseWorldPos = GetMouseWorldPosition(eventData);
        offset = transform.position - mouseWorldPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        isDragging = false;

        // ドロップ処理を追加（例：Boxに吸着させる）
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(needZoom && !StageSwitcher.I.mapManager.IsZoom)
        {
            return;
        }

        if (!isDragging)
            return;

        Vector3 mouseWorldPos = GetMouseWorldPosition(eventData);
        transform.position = mouseWorldPos + offset;
    }

    private Vector3 GetMouseWorldPosition(PointerEventData eventData)
    {
        Vector3 screenPos = eventData.position;
        screenPos.z = Mathf.Abs(mainCamera.WorldToScreenPoint(transform.position).z);
        return mainCamera.ScreenToWorldPoint(screenPos);
    }
}
