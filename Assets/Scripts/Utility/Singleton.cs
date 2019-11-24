using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace game.utility
{
    public class Singleton<T> where T : Singleton<T>
    {
        static T s_instance;

        public static T instance
        {
            get
            {
                if (s_instance == null)
                {
                    Type type = typeof(T);
                    ConstructorInfo ctor;
                    ctor = type.GetConstructor(
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                        null, new Type[0], new ParameterModifier[0]);
                    s_instance = (T)ctor.Invoke(new object[0]);
                    s_instance.init();

                }
                return s_instance;
            }
        }

        protected virtual void init() { }
    }

    public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : SingletonMonoBehavior<T>
    {
        public static T instance
        {
            get
            {
                return s_instance;
            }
        }

        private static T s_instance = null;
        private static int instance_count = 0;

        protected virtual void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this as T;
                s_instance.Init();
            }
            else
            {
                Destroy(this);
            }

            ++instance_count;
        }

        protected virtual void OnDestroy()
        {
            --instance_count;
            if (instance_count == 0)
            {
                s_instance = null;
            }
        }

        protected virtual void Init() { }
    }

    public abstract class SingletonMonoBehaviorNoDestroy<T> : MonoBehaviour where T : SingletonMonoBehaviorNoDestroy<T>
    {
        public static T instance
        {
            get
            {
                return s_instance;
            }
        }

        private static T s_instance = null;

        protected virtual void Awake()
        {
            if (s_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                s_instance = this as T;
                Init();
            }
            else
            {
                Destroy(this);
            }
        }

        protected virtual void Init() { }

        public static void ReleaseInstance()
        {
            if (s_instance != null)
            {
                Destroy(s_instance);
                s_instance = null;
            }
        }
    }
}

