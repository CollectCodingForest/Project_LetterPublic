using System;
using System.Collections;
using System.Collections.Generic;

public class EventSubject
{
    // Observer pattern

    public event Action OnUpdateUI;
    public event Action OnEndCustomer;
    public event Action OnCounter;
    public event Action OnFailAlarm;


    public void EventUpdateUI()
    {
        OnUpdateUI?.Invoke();
    }
    public void EventEndCustomer()
    {
        OnEndCustomer?.Invoke();
    }
    public void EventCounter()
    {
        OnCounter?.Invoke();
    }
    public void EventFailAlarm()
    {
        OnFailAlarm?.Invoke();
    }

}
