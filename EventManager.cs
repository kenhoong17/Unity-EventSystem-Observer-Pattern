using System;
using System.Collections.Generic;
using UnityEngine;

namespace XTT
{
    public class Event { }
    public delegate void EventAction<T>(T evt) where T : Event;

    public static class EventManager
    {
        // =================================================================================================================

        #region Event Variables

        private static Dictionary<Type, EventAction<Event>> _LISTENER_DICT = new Dictionary<Type, EventAction<Event>>();
        private static Dictionary<Delegate, EventAction<Event>> _LOOKUP_DICT = new Dictionary<Delegate, EventAction<Event>>();

        private static Dictionary<Type, EventAction<Event>> _DONT_DESTROY_LISTENER_DICT = new Dictionary<Type, EventAction<Event>>();
        private static Dictionary<Delegate, EventAction<Event>> _DONT_DESTROY_LOOKUP_DICT = new Dictionary<Delegate, EventAction<Event>>();

        #endregion Event Variables

        // =================================================================================================================

        #region Event Functions

        public static void AddEvent(Event evt)
        {
            if (_LISTENER_DICT.TryGetValue(evt.GetType(), out EventAction<Event> listener))
            {
                listener.Invoke(evt);
            }

            if (_DONT_DESTROY_LISTENER_DICT.TryGetValue(evt.GetType(), out EventAction<Event> dontDestroyListener))
            {
                dontDestroyListener.Invoke(evt);
            }

        }

        public static void AddListener<T>(EventAction<T> listener) where T : Event
        {
            // To avoid multiple registration of same listener
            if (_LOOKUP_DICT.ContainsKey(listener))
                return;

            EventAction<Event> actualListener = (evt) => listener(evt as T);
            _LOOKUP_DICT[listener] = actualListener;

            Type type = typeof(T);

            if (_LISTENER_DICT.TryGetValue(type, out EventAction<Event> tempListener))
            {
                _LISTENER_DICT[type] = tempListener += actualListener;
            }
            else
            {
                _LISTENER_DICT[type] = actualListener;
            }
        }

        public static void RemoveListener<T>(EventAction<T> listener) where T : Event
        {
            if (_LOOKUP_DICT.TryGetValue(listener, out EventAction<Event> actualListener))
            {
                Type type = typeof(T);

                if (_LISTENER_DICT.TryGetValue(type, out EventAction<Event> tempListener))
                {
                    tempListener -= actualListener;
                    if (tempListener == null)
                    {
                        _LISTENER_DICT.Remove(type);
                    }
                    else
                    {
                        _LISTENER_DICT[type] = tempListener;
                    }
                }
                _LOOKUP_DICT.Remove(listener);
            }
        }

        public static void AddDontDestroyListener<T>(EventAction<T> listener) where T : Event
        {
            // To avoid multiple registration of same listener
            if (_DONT_DESTROY_LOOKUP_DICT.ContainsKey(listener))
            {
                return;
            }

            EventAction<Event> actualListener = (evt) => listener(evt as T);
            _DONT_DESTROY_LOOKUP_DICT[listener] = actualListener;

            Type type = typeof(T);

            if (_DONT_DESTROY_LISTENER_DICT.TryGetValue(type, out EventAction<Event> tempListener))
            {
                _DONT_DESTROY_LISTENER_DICT[type] = tempListener += actualListener;
            }
            else
            {
                _DONT_DESTROY_LISTENER_DICT[type] = actualListener;
            }
        }

        public static void RemoveDontDestroyListener<T>(EventAction<T> listener) where T : Event
        {
            if (_DONT_DESTROY_LOOKUP_DICT.TryGetValue(listener, out EventAction<Event> actualListener))
            {
                Type type = typeof(T);

                if (_DONT_DESTROY_LISTENER_DICT.TryGetValue(type, out EventAction<Event> tempListener))
                {
                    tempListener -= actualListener;
                    if (tempListener == null)
                    {
                        _DONT_DESTROY_LISTENER_DICT.Remove(type);
                    }
                    else
                    {
                        _DONT_DESTROY_LISTENER_DICT[type] = tempListener;
                    }
                }
                _DONT_DESTROY_LOOKUP_DICT.Remove(listener);
            }
        }

        #endregion Event Functions

        // =================================================================================================================

    }
}