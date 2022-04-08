using System;
using System.Collections.Generic;

public class Subject : ISubject
{
    public GameEvent state;
    private List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }
    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.UpdateData(state);
        }
    }
}
