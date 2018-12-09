//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    private static Dictionary<System.Type, Object> m_Services = new Dictionary<System.Type, Object>();
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public static void RegisterService<T>(Object obj)
    {
        System.Type type = typeof(T);
        if (m_Services.ContainsKey(type))
        {
            m_Services[type] = obj;
            Debug.LogError("You are registering a serive on top of an other.");
        }
        else
        {
            m_Services.Add(type, obj);
        }
    }

    public static void UnregisterService<T>(Object obj)
    {
        System.Type type = typeof(T);
        if (m_Services.ContainsKey(type))
        {
            m_Services.Remove(type);           
        }
        else
        {
            Debug.LogError("You are unregistering a serive that is not currently registered.");
        }
    }

    public static Object GetService<T>()
    {
        System.Type type = typeof(T);
        if (m_Services.ContainsKey(type))
        {
            return m_Services[type];
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
