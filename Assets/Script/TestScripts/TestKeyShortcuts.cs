//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CollectionHelper;

public class TestKeyShortcuts : MonoBehaviour {

    [System.Serializable]
    public struct RegisteredShortcuts
    {
        public KeyCode Key;
        public Action function;
        public string Name;
        public string Description;
        public string ClassName;

        public RegisteredShortcuts(KeyCode key, Action action, string name, string description, string className)
        {
            Key = key;
            function = action;
            Name = name;
            Description = description;
            ClassName = className;
        }
    }
    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static TestKeyShortcuts instance;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    List<RegisteredShortcuts> shortcuts;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        shortcuts.Clear();
    }

    protected void update()
    {
#if UNITY_EDITOR
        foreach (RegisteredShortcuts rs in shortcuts)
        {
            if (Input.GetKeyDown(rs.Key))
            {
                rs.function();
            }
        }
#endif
    }


    #endregion

    #region Public API
    public void Add(RegisteredShortcuts rs)
    {
        shortcuts.AddUnique(rs);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
