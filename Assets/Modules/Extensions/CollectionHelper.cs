//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreUtility
{
    [System.Serializable]
    public class PercentageList<T>
    {
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
        protected List<T> m_List = new List<T>();
        protected List<float> m_Percentages = new List<float>();
        ////////////////////////////////
        ///			Private			 ///
        ////////////////////////////////

        #region Unity API
        #endregion

        #region Public API
        public T Random()
        {
            float range = 0f;
            for (int i = 0; i < m_Percentages.Count; ++i)
            {
                range += m_Percentages[i];
            }

            float randomed = UnityEngine.Random.Range(0f, range);
            float currentVal = 0f;
            for (int i = 0; i < m_Percentages.Count; ++i)
            {
                currentVal += m_Percentages[i];
                if (currentVal >= randomed)
                {
                    return m_List[i];
                }
            }
            Debug.LogError("Error in randomer.");
            return m_List[m_List.Count];
        }

        public void Add(T item, float percentage)
        {
            m_List.Add(item);
            m_Percentages.Add(percentage);
        }

        public void Remove(T item)
        {
            int index = m_List.IndexOf(item);
            if (index != -1)
            {
                m_List.RemoveAt(index);
                m_Percentages.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < m_List.Count)
            {
                m_List.RemoveAt(index);
                m_Percentages.RemoveAt(index);
            }
        }
        #endregion

        #region Protect
        #endregion

        #region Private
        #endregion
    }

}

namespace CollectionHelper {

    public static class ListHelper
    {
        public static void AddUnique<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
            else
            {
                Debug.LogWarning("Element already in collection.");
            }
        }
    }
}
