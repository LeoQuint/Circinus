//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

using TaskType = AITask.TaskType;

[System.Serializable]
[XmlRoot("AIConfig")]
public class AIConfig: Config {

    public static int NUMBER_OF_PRIORITY_LEVELS = 5;

    public override string Filename
    {
        get
        {
            return "AIConfig";
        }
    }
    public List<List<TaskType>> TaskPriorities = new List<List<TaskType>>();
}
