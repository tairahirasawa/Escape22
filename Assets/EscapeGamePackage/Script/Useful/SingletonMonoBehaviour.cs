using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T I { get; private set; }

    [HideInInspector] public static bool IsInstanced;
    [HideInInspector] public bool DontDestroyEnabled;
    
    private void Awake()
    {
        IsInstanced = false;
        DontDestroyEnabled = false;

        if (I == null)
        {
            I = this as T;

            IsInstanced = true;
            OnAwake();

            if (DontDestroyEnabled)
            {
                DontDestroyOnLoad(gameObject);
            }

            return;
        }

        Destroy(this);
    }

    private void Start()
    {
        OnStart();
    }


    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }

    private void OnDestroy()
    {
        if (I == this)
        {
            I = null;
        }
    }

}