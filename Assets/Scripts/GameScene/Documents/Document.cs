using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Document : MonoBehaviour
{
    protected CustomerData customerData;

    protected virtual void Start()
    {
        GameSceneManager.Instance.eventSubject.OnUpdateUI += UpdateUI;
    }
    protected virtual void OnDestroy()
    {
        GameSceneManager.Instance.eventSubject.OnUpdateUI -= UpdateUI;
    }

    protected abstract void UpdateUI();

}

