//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using UnityEditor;
using UnityEngine;

public class TestDataStore : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    private string m_FileName = "savedData";
    [SerializeField]
    private TestData testData;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    [System.Serializable]
    public struct TestData
    {
        public string name;
        public int number;
        public Vector3 position;
    }

    #region Unity API
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("File saved");
            Serializer_Deserializer<TestData> sd = new Serializer_Deserializer<TestData>(testData, Serializer_Deserializer<TestData>.SavedPath.GameData,  m_FileName);
            sd.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("File Loaded");
            Serializer_Deserializer<TestData> sd = new Serializer_Deserializer<TestData>(testData, Serializer_Deserializer<TestData>.SavedPath.GameData, m_FileName);
            testData = sd.Load();
        }
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
