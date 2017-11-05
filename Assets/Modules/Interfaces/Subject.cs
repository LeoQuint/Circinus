//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectionHelper;

public enum NoticifationType
{
    OnDestroy,
    Message,
    Null
}

public class Subject: MonoBehaviour {

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private List<Observer> m_Observers = new List<Observer>();    

    #region Unity API

    private void OnDestroy()
    {
        Notify(NoticifationType.OnDestroy);
    }

    #endregion

    #region Public API

    public void Register(Observer obs)
    {
        m_Observers.AddUnique(obs);
    }

    public void Unregister(Observer obs)
    {
        m_Observers.Remove(obs);
    }

    #endregion

    #region Protect

    protected void Notify(params object[] notice)
    {
        for (int i = 0; i < m_Observers.Count; i++)
        {
            m_Observers[i].OnNotify(notice);
        }
    }

    #endregion

    #region Private
    #endregion
}
