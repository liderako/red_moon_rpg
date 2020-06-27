using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedMoonRPG.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance 
        {
            get { return instance;}
        }

        public static bool IsInitialized()
        {
            return instance != null;
        }

        protected virtual void Awake()
        {
            if (instance == null) 
            {
                instance = (T)this;
            }
            else
            {
                Debug.LogError("[Singleton] trying to instantiate a second instance of singleton class.");
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;    
            }    
        }
    }
}
