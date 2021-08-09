﻿using System;
using System.Collections.Generic;

public class SignalBus
{
    private readonly Dictionary<Type, List<SignalSubscriptionWrapper>> _SignalMap = new  Dictionary<Type, List<SignalSubscriptionWrapper>>();
    
    public void Subscribe<T>(Action<T> onSignalAction, object identifier)
    {
        if (!_SignalMap.TryGetValue(typeof(T), out var actionList))
        {
            actionList = new List<SignalSubscriptionWrapper>();
            _SignalMap.Add(typeof(T), actionList);
        }
        actionList.Add(new SignalSubscription<T>(onSignalAction, identifier));
    }

    public void FireSignal<T>(T signal)
    {
        if (_SignalMap.TryGetValue(typeof(T), out var actionList))
        {
            foreach (var action in actionList)
            {
                ((SignalSubscription<T>)action).Callback?.Invoke(signal);
            }
        }
    }
}

internal class SignalSubscription<T> : SignalSubscriptionWrapper {
    public readonly Action<T> Callback;
    public override object Identifier { get; }
        
    public SignalSubscription(Action<T> callback, object identifier) {
        Callback = callback;
        Identifier = identifier;
    }
}
internal abstract class SignalSubscriptionWrapper
{
    public abstract object Identifier { get; }
}