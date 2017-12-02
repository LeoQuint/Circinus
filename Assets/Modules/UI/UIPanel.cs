//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class UIPanel : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [Header("Positioning")]
    [SerializeField]
    private Vector2 m_HiddenOffset = Vector2.zero;
    [Header("Animation")]
    [SerializeField]
    private float m_AnimationDuration = 0.3f;
    [SerializeField]
    private Ease m_EaseType = Ease.InOutExpo;
    [Header("Behaviour")]
    [SerializeField]
    private bool m_HideOnAwake = true;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    private RectTransform rectTransform;
    private Vector2 m_OriginalPosition;
    private bool m_IsHidden = false;
    private bool m_IsHidding = false;
    private bool m_IsShowing = false;
    //Callbacks
    private Action<bool> m_OnAnimationComplete;
    //Tweens
    private Tweener m_Tweener = null;

    ///Properties
    ///
    public bool IsHidden
    {
        get { return m_IsHidden; }
    }

    public Action<bool> OnAnimationDone
    {
        get { return m_OnAnimationComplete; }
        set { m_OnAnimationComplete = value; }
    }

    public TweenCallback OnTweenKilled
    {
        get
        {
            if (m_Tweener != null)
            {
                return m_Tweener.onKill;
            }
            return null;
        }
        set
        {
            if (m_Tweener != null)
            {
                m_Tweener.onKill += value;
            }            
        }
    }

    #region Unity API
    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        m_OriginalPosition = rectTransform.anchoredPosition;
        if (m_HideOnAwake)
        {
            Hide(true);
        }
    }

    protected void OnDestroy()
    {
        if (m_OnAnimationComplete != null)
        {
            m_OnAnimationComplete = null;
        }
    }
    #endregion

    #region Public API
    public void Show(bool immediate = false, Action<bool> OnAnimationDone = null)
    {
        m_IsHidden = false;
        if (OnAnimationDone != null)
        {
            m_OnAnimationComplete += OnAnimationDone;
        }

        if (immediate)
        {
            KillAllTweens();
            rectTransform.anchoredPosition = m_OriginalPosition;
            OnDone();
        }
        else
        {
            if (!m_IsShowing)            
            {
                KillAllTweens();
                m_IsShowing = true;
                m_Tweener = rectTransform.DOLocalMove(m_OriginalPosition, m_AnimationDuration);
                m_Tweener.SetEase(m_EaseType);
                m_Tweener.OnComplete(OnDone);
            }           
        }
    }

    public void Hide(bool immediate = false, Action<bool> OnAnimationDone = null)
    {
        m_IsHidden = true;
        if (OnAnimationDone != null)
        {
            m_OnAnimationComplete += OnAnimationDone;
        }

        if (immediate)
        {
            KillAllTweens();
            rectTransform.anchoredPosition = m_OriginalPosition + m_HiddenOffset;
            OnDone();
        }
        else
        {
            if (!m_IsHidding)
            {
                KillAllTweens();
                m_IsHidding = true;
                m_Tweener = rectTransform.DOLocalMove(m_OriginalPosition + m_HiddenOffset, m_AnimationDuration);
                m_Tweener.SetEase(m_EaseType);
                m_Tweener.OnComplete(OnDone);
            }
        }     
    }
    #endregion

    #region Protect
    protected void OnDone()
    {
        m_IsShowing = false;
        m_IsHidding = false;
        if (m_Tweener != null)
        {
            m_Tweener.onComplete = null;
            m_Tweener = null;
        }
        if (m_OnAnimationComplete != null)
        {
            m_OnAnimationComplete(m_IsHidden);
        }
    }
    #endregion

    #region Private
    private void KillAllTweens()
    {
        if (m_Tweener != null)
        {
            m_Tweener.Kill();
        }
    }
    #endregion
}
