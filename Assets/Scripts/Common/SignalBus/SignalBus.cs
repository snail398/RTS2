using System;
using System.Collections.Generic;
using System.Linq;

    public sealed class SignalBus {
        private readonly Dictionary<Type, List<SignalSubscriptionWrapper>> _SubscriptionsMap = new Dictionary<Type, List<SignalSubscriptionWrapper>>();

        public void Subscribe<TSignal>(Action<TSignal> callback, object identifier) {
            List<SignalSubscriptionWrapper> subscriptions;
            if (!_SubscriptionsMap.TryGetValue(typeof(TSignal), out subscriptions)) {
                subscriptions = new List<SignalSubscriptionWrapper>();
                _SubscriptionsMap.Add(typeof(TSignal), subscriptions);
            }
            subscriptions.Add(new SignalSubscription<TSignal>(callback, identifier));
        }

        public void SubscribeDynamic(Action<object> callback, Type type, object identifier) {
            var subsMethod = GetType().GetMethod("Subscribe").MakeGenericMethod(type);
            subsMethod.Invoke(this, new[]{callback, identifier});
        } 
        
        public void UnSubscribe<TSignal>(object identifier) {
            List<SignalSubscriptionWrapper> subscriptions;
            if (!_SubscriptionsMap.TryGetValue(typeof(TSignal), out subscriptions)) {
                return;
            }
            subscriptions.RemoveAll(_ => _.Identifier == identifier);
        }

        //concrete version is more performant
        public void UnSubscribeFromAll(object identifier) {
            foreach (var signalsSubscriptions in _SubscriptionsMap) {
                signalsSubscriptions.Value.RemoveAll(_ => _.Identifier == identifier);
            }
        }

        public void FireDynamicSignal(object signal) {
            if (signal == null) {
                return;
            }

            var m = GetType()?.GetMethod("FireSignal");
            if (m == null) {
                return;
            }
            var fireMethod = m.MakeGenericMethod(signal.GetType());
            fireMethod?.Invoke(this, new[]{signal});
        } 
        
        public void FireSignal<TSignal>(TSignal signal) {
            var subscriptions = GetSignalSubscriptions<TSignal>();
            var count = subscriptions?.Count;
            for (int i = 0; i < count; i++) {
                ((SignalSubscription<TSignal>)subscriptions[i]).Callback.Invoke(signal);
            }
        }

        /// method uses copy of subscriptions to prevent original collection modification on enumeration
        public void FireSignalSafe<TSignal>(TSignal signal) {
            var subscriptions = GetSignalSubscriptions<TSignal>()?.ToList();
            subscriptions?.ForEach(_ => {
                ((SignalSubscription<TSignal>)_).Callback.Invoke(signal);
            });
        }

        private List<SignalSubscriptionWrapper> GetSignalSubscriptions<TSignal>() {
            if (!_SubscriptionsMap.TryGetValue(typeof(TSignal), out var subscriptions)) {

                return null;
            }
            return subscriptions;
        }
    }
