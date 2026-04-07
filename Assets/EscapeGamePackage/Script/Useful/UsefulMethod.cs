using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class UsefulMethod
{
    public static void Present(CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public static void Hide(CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    public static void DisableTapCanvasGroup(CanvasGroup canvas)
    {
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    public static void EnableTapCanvasGroup(CanvasGroup canvas)
    {
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public static void SetActiveList(List<GameObject> list,bool value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(value);
        }
    }

    /// <summary>
    /// 指定されたインターフェイスを実装している物を探してリストを作る
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] FindObjectOfInterfaces<T>() where T : class
    {
        List<T> list = new List<T>();
        foreach (var n in Resources.FindObjectsOfTypeAll<Component>())
        {
            var component = n as T;
            if (component != null)
            {
                list.Add(component);
            }
        }
        T[] ret = new T[list.Count];
        int count = 0;
        foreach (T component in list)
        {
            ret[count] = component;
            count++;
        }
        return ret;
    }


}
