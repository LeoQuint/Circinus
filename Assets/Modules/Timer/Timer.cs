//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CoreUtility
{ 
    public class Timer {

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
        public Action<float> OnUpdate;
        public Action OnDone;
        ////////////////////////////////
        ///			Protected		 ///
        ////////////////////////////////
        protected float m_Elapsed;
        protected float m_Duration;
        protected bool m_UseScaledTime = true;
        protected bool m_IsStarted = false;
        protected bool m_IsDone = false;
        ////////////////////////////////
        ///			Private			 ///
        ////////////////////////////////

        public float Ratio
        {
            get { return Mathf.Clamp01( m_Elapsed / m_Duration);  }
        }
        #region Unity API
        #endregion

        #region Public API
        public Timer(float duration, bool useScaledTime = true)
        {
            m_Elapsed = 0f;
            m_UseScaledTime = useScaledTime;
            m_Duration = duration;
            m_IsDone = false;
            m_IsStarted = false;
        }

        public void Update()
        {
            if (m_IsStarted && !m_IsDone)
            {
                if (m_UseScaledTime)
                {
                    m_Elapsed += Time.deltaTime;
                }
                else
                {
                    m_Elapsed += Time.unscaledDeltaTime;
                }

                if (OnUpdate != null)
                {
                    OnUpdate(Ratio);
                }

                if (m_Elapsed >= m_Duration)
                {
                    OnTimerDone();
                }
            }
        }

        public void Start()
        {
            m_Elapsed = 0f;
            m_IsDone = false;
            m_IsStarted = true;
        }

        public void Start(float duration)
        {
            m_Duration = duration;
            Start();
        }

        public void Stop()
        {
            m_IsDone = false;
            m_IsStarted = false;
        }
        #endregion

        #region Protect
        protected void OnTimerDone()
        {
            m_IsDone = true;

            if (OnDone != null)
            {
                OnDone();
            }
        }
        #endregion

        #region Private
        #endregion
    }
}