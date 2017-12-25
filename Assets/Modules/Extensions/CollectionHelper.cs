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
        protected List<ItemPercentage<T>> m_List = new List<ItemPercentage<T>>();
        ////////////////////////////////
        ///			Private			 ///
        ////////////////////////////////

        #region Unity API
        #endregion

        #region Public API
        public T Random()
        {
            float range = 0f;
            for (int i = 0; i < m_List.Count; ++i)
            {
                range += m_List[i].percentage;
            }

            float randomed = UnityEngine.Random.Range(0f, range);
            float currentVal = 0f;
            for (int i = 0; i < m_List.Count; ++i)
            {
                currentVal += m_List[i].percentage;
                if (currentVal >= randomed)
                {
                    return m_List[i].item;
                }
            }
            Debug.LogError("Error in randomer.");
            return m_List[m_List.Count].item;
        }

        public void Add(T item, float percentage)
        {
            m_List.Add(new ItemPercentage<T>(item, percentage));
        }
        /// <summary>
        /// Removes the first element found.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {            
            for (int i = 0; i < m_List.Count; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(item, m_List[i].item))
                {
                    m_List.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (index < m_List.Count)
            {
                m_List.RemoveAt(index);
            }
        }
        #endregion

        #region Protect
        #endregion

        #region Private
        #endregion
    }

    [System.Serializable]
    public struct ItemPercentage<T>
    {
        public T item;
        public float percentage;

        public ItemPercentage(T item, float percent)
        {
            this.item = item;
            this.percentage = percent;
        }
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
