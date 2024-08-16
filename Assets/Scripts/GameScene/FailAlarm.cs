using System;
using UnityEngine;

public class FailAlarm : MonoBehaviour
{
    
    private void Start()
    {
        GameSceneManager.Instance.eventSubject.OnFailAlarm += Alarm;
    }

    private void Alarm()
    {
        int i = GameSceneManager.Instance.playData.Customer;
        //Debug.Log($"{GameSceneManager.Instance.wrongPart[0]}"); 
    }
}