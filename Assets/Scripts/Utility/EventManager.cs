using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.utility
{
    public enum EventGroup : short
    {
        NONE = 0,
        SCENE =1,
    }

    public enum SceneEvent : short
    {
        SCENE_IN = 0,
        SCENE_OUT,
    }


    // Note: 事件的簽章會以第一個註冊的函式為準。
    // Note: 如果Delegate被清空之後註冊一個不同簽章的函式，不會導致exception，但會使原來發送的事件接收不到。
    public class EventHandler
    {
        private Dictionary<int, Delegate> callbackDict = new Dictionary<int, Delegate>(521);

        public void Register(int id, Action callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action callbacks)
            {
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                Debug.LogError("[EventHandler] Cannot register different types of callback functions in the same Event ID");
            }
        }

        public void Unregister(int id, Action callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Send(int id)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            (del as Action)?.Invoke();
        }

        public void Register<T>(int id, Action<T> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T> callbacks)
            {
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                Debug.LogError("[EventHandler] Cannot register different types of callback functions in the same Event ID");
            }
        }

        public void Unregister<T>(int id, Action<T> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Send<T>(int id, T arg)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            (del as Action<T>)?.Invoke(arg);
        }

        public void Register<T1, T2>(int id, Action<T1, T2> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T1, T2> callbacks)
            {
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                Debug.LogError("[EventHandler] Cannot register different types of callback functions in the same Event ID");
            }
        }

        public void Unregister<T1, T2>(int id, Action<T1, T2> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Send<T1, T2>(int id, T1 arg1, T2 arg2)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            (del as Action<T1, T2>)?.Invoke(arg1, arg2);
        }

        public void Register<T1, T2, T3>(int id, Action<T1, T2, T3> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del == null)
            {
                callbackDict[id] = callback;
                return;
            }

            if (del is Action<T1, T2, T3> callbacks)
            {
                callbackDict[id] = callbacks + callback;
            }
            else
            {
                Debug.LogError("[EventHandler] Cannot register different types of callback functions in the same Event ID");
            }
        }

        public void Unregister<T1, T2, T3>(int id, Action<T1, T2, T3> callback)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            if (del is Action<T1, T2, T3> callbacks)
            {
                callbackDict[id] = callbacks - callback;
            }
        }

        public void Send<T1, T2, T3>(int id, T1 arg1, T2 arg2, T3 arg3)
        {
            callbackDict.TryGetValue(id, out Delegate del);
            (del as Action<T1, T2, T3>)?.Invoke(arg1, arg2, arg3);
        }

        public void Clear()
        {
            callbackDict.Clear();
        }
    }

    public class EventManager : Singleton<EventManager>
    {
        public static int CombineId(short hi, short lo)
        {
            return hi << 16 | (ushort)lo;
        }

        private EventHandler eventHandler = new EventHandler();

        public void Register(EventGroup groupId, short eventId, Action callback)
        {
            eventHandler.Register(CombineId((short)groupId, eventId), callback);
        }

        public void Unregister(EventGroup groupId, short eventId, Action callback)
        {
            eventHandler.Unregister(CombineId((short)groupId, eventId), callback);
        }

        public void Send(EventGroup groupId, short eventId)
        {
            eventHandler.Send(CombineId((short)groupId, eventId));
        }

        public void Register<T>(EventGroup groupId, short eventId, Action<T> callback)
        {
            eventHandler.Register(CombineId((short)groupId, eventId), callback);
        }

        public void Unregister<T>(EventGroup groupId, short eventId, Action<T> callback)
        {
            eventHandler.Unregister(CombineId((short)groupId, eventId), callback);
        }

        public void Send<T>(EventGroup groupId, short eventId, T arg1)
        {
            eventHandler.Send(CombineId((short)groupId, eventId), arg1);
        }

        public void Register<T1, T2>(EventGroup groupId, short eventId, Action<T1, T2> callback)
        {
            eventHandler.Register(CombineId((short)groupId, eventId), callback);
        }

        public void Unregister<T1, T2>(EventGroup groupId, short eventId, Action<T1, T2> callback)
        {
            eventHandler.Unregister(CombineId((short)groupId, eventId), callback);
        }

        public void Send<T1, T2>(EventGroup groupId, short eventId, T1 arg1, T2 agr2)
        {
            eventHandler.Send(CombineId((short)groupId, eventId), arg1, agr2);
        }

        public void Register<T1, T2, T3>(EventGroup groupId, short eventId, Action<T1, T2, T3> callback)
        {
            eventHandler.Register(CombineId((short)groupId, eventId), callback);
        }

        public void Unregister<T1, T2, T3>(EventGroup groupId, short eventId, Action<T1, T2, T3> callback)
        {
            eventHandler.Unregister(CombineId((short)groupId, eventId), callback);
        }

        public void Send<T1, T2, T3>(EventGroup groupId, short eventId, T1 arg1, T2 agr2, T3 agr3)
        {
            eventHandler.Send(CombineId((short)groupId, eventId), arg1, agr2, agr3);
        }
    }
}

