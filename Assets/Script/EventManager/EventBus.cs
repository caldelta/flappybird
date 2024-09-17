using System.Collections.Generic;
using EventManager.Model;
using UnityEngine;
using UnityEngine.Events;

namespace EventManager
{
    public class EventBus : SingletonMonoBehaviour<EventBus>
    {
        private Dictionary<EventBusType, UnityEvent> m_eventDictionary;

        public Dictionary<EventBusType, UnityEvent> EventDictionary
        {
            get
            {
                return m_eventDictionary;
            }
        }

        public override void Awake()
        {
            base.Awake();
            Init();
#if DEBUG
            Debug.Log("[EventBus]: Init");
#endif
        }

        private void Init()
        {
            if(m_eventDictionary == null)
            {
                m_eventDictionary = new Dictionary<EventBusType, UnityEvent>();
            }
        }

        public void StartListening(EventBusType eventName, UnityAction callback)
        {
            if(m_eventDictionary.TryGetValue(eventName, out UnityEvent unityEvent))
            {
                unityEvent.AddListener(callback);
            }
            else
            {
                unityEvent = new UnityEvent();
                unityEvent.AddListener(callback);
                m_eventDictionary.Add(eventName, unityEvent);
            }
#if DEBUG
            System.Diagnostics.StackFrame objStackFrame = new System.Diagnostics.StackFrame(1);
            string className = objStackFrame.GetMethod().ReflectedType.FullName;
            //Debug.Log($"[EventBus]: StartListening {eventName} from {className}");
#endif
        }

        public void StopListening(EventBusType eventName, UnityAction callback)
        {
            if (m_eventDictionary.TryGetValue(eventName, out UnityEvent unityEvent))
            {
                unityEvent.RemoveListener(callback);
#if DEBUG
                //Debug.Log("[EventBus]: StopListening " + eventName);
#endif
            }
        }

        public void StopAllListening()
        {
            foreach (var pair in m_eventDictionary)
            {
                m_eventDictionary[pair.Key].RemoveAllListeners();
            }
#if DEBUG
            Debug.Log("[EventBus]: StopAllListening");
#endif
        }

        public void FireEvent(EventBusType eventName)
        {
            if(m_eventDictionary.TryGetValue(eventName, out UnityEvent unityEvent))
            {
                unityEvent.Invoke();
#if DEBUG
                System.Diagnostics.StackFrame objStackFrame = new System.Diagnostics.StackFrame(1);
                string className = objStackFrame.GetMethod().ReflectedType.FullName;
                //Debug.Log($"[EventBus]: FireEvent {eventName} from {className}");
#endif
            }
        }
    }
}